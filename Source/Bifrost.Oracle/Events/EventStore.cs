using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bifrost.Events;
using Bifrost.Serialization;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Bifrost.Oracle.Events
{
    public class EventStore : IEventStore
    {
        const string SELECT_INFO_FOR_EVENT =
            "SELECT ID, COMMANDCONTEXT, NAME, LOGICALNAME, EVENTSOURCEID, EVENTSOURCE, GENERATION, DATA, CAUSEDBY, ORIGIN, OCCURED, VERSION" +
            " FROM EVENTS";

        const string READ_ALL_EVENTS = SELECT_INFO_FOR_EVENT + " ORDER BY ID ASC";

        const string INSERT_STATEMENT =
            "INSERT INTO EVENTS (COMMANDCONTEXT,NAME,LOGICALNAME,EVENTSOURCEID,EVENTSOURCE,GENERATION,DATA,CAUSEDBY,ORIGIN,OCCURED,VERSION)"
            + " VALUES(:COMMANDCONTEXT_{0},:NAME_{0},:LOGICALNAME_{0},:EVENTSOURCEID_{0},:EVENTSOURCE_{0},:GENERATION_{0},:DATA_{0},:CAUSEDBY_{0},:ORIGIN_{0},:OCCURED_{0},:VERSION_{0})"
            + " RETURNING ID INTO :ID_{0};";

        const string READ_STATEMENT_FOR_EVENTS_BY_AGGREGATE_ROOT =
            SELECT_INFO_FOR_EVENT + " WHERE EVENTSOURCE = :EVENTSOURCE AND EVENTSOURCEID = :EVENTSOURCEID";

        const string READ_STATEMENT_FOR_EVENTS_BY_PAGE =
        "SELECT a.ID, a.COMMANDCONTEXT, a.NAME, a.LOGICALNAME, a.EVENTSOURCEID, a.EVENTSOURCE, a.GENERATION, a.DATA, a.CAUSEDBY, a.ORIGIN, a.OCCURED, a.VERSION FROM"
            + " (SELECT b.ID, b.COMMANDCONTEXT, b.NAME, b.LOGICALNAME, b.EVENTSOURCEID, b.EVENTSOURCE, b.GENERATION, b.DATA, b.CAUSEDBY, b.ORIGIN, b.OCCURED, b.VERSION, rownum b_rownum"
            + " FROM (SELECT c.ID, c.COMMANDCONTEXT, c.NAME, c.LOGICALNAME, c.EVENTSOURCEID, c.EVENTSOURCE, c.GENERATION, c.DATA, c.CAUSEDBY, c.ORIGIN, c.OCCURED, c.VERSION FROM EVENTS c ORDER BY ID ASC) b"
            + " WHERE rownum <= :END_OF_BATCH) a"
            + " WHERE b_rownum >= :START_OF_BATCH";
 

        const string LAST_VERSION_STATEMENT =
            "SELECT VERSION" +
            " FROM EVENTS" +
            " WHERE EVENTSOURCE = :EVENTSOURCE AND EVENTSOURCEID = :EVENTSOURCEID" +
            " AND ROWNUM = 1 " +
            " ORDER BY VERSION DESCENDING";


        readonly OracleConnection _connection;
        readonly IEventMigratorManager _eventMigratorManager;
        readonly IEventMigrationHierarchyManager _eventMigrationHierarchyManager;
        readonly ISerializer _serializer;
        readonly IEventParameters _eventParameters;

        public EventStore(EventStoreConfiguration configuration, IEventMigratorManager eventMigratorManager, IEventMigrationHierarchyManager eventMigrationHierarchyManager,
                                    ISerializer serializer, IEventParameters eventParameters)
        {
            _eventMigratorManager = eventMigratorManager;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            _serializer = serializer;
            _eventParameters = eventParameters;
            _connection = configuration.Connection;
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var events = uncommittedEventStream.Select(e => e).ToArray();
            var parameters = new List<OracleParameter>();
            var insertStatementBuilder = new StringBuilder();

            insertStatementBuilder.Append("BEGIN ");
            for (var position = 0; position < events.Length; position++)
            {
                insertStatementBuilder.Append(GetParameterizedInsertStatement(position));
                parameters.AddRange(_eventParameters.BuildFromEvent(position, events[position]));
            }
            insertStatementBuilder.Append(" END;");

            Tuple<int, long>[] returnedIds;

            try
            {
                OpenConnection();
                var transaction = _connection.BeginTransaction();
                try
                {
                    using (var command = _connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = insertStatementBuilder.ToString();
                        command.Parameters.AddRange(parameters.ToArray());
                        command.BindByName = true;
                        command.ExecuteNonQuery();

                        returnedIds = ExtractReturnedIds(parameters);
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            finally
            {
                EnsureConnectionClosed(_connection);
            }

            PopulateEventId(returnedIds, events);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
            if (events.Any())
                committedEventStream.Append(events);
            return committedEventStream;
        }

        static Tuple<int, long>[] ExtractReturnedIds(List<OracleParameter> parameters)
        {
            return parameters.Where(p => p.ParameterName.StartsWith("ID_"))
                                .Select(p => new Tuple<int, long>(int.Parse(p.ParameterName.Substring(3)),
                                                                    ((OracleDecimal)p.Value).ToInt64()))
                                .ToArray();
        }

        void PopulateEventId(Tuple<int, long>[] returnedIds, IEvent[] events)
        {
            for (var i = 0; i < returnedIds.Length; i++)
            {
                events[i].Id = returnedIds[i].Item2;
            }
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            var eventDtos = new List<EventDto>();

            var start = batchesToSkip*batchSize + 1;
            var end = batchesToSkip*batchSize + batchSize;

            var startParam = new OracleParameter("START_OF_BATCH", OracleDbType.Int32, 510);
            startParam.Value = start;

            var endParam = new OracleParameter("END_OF_BATCH", OracleDbType.Int32, 10);
            endParam.Value = end;

            try
            {
                OpenConnection();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = READ_STATEMENT_FOR_EVENTS_BY_PAGE;
                    command.Parameters.Add(startParam);
                    command.Parameters.Add(endParam);
                    command.BindByName = true;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        eventDtos.Add(MapReaderToEventDto(reader));
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed(_connection);
            }

            return eventDtos.Select(BuildEventInstanceFromDto)
                                    .Select(@event => _eventMigratorManager.Migrate(@event))
                                    .ToArray();
        }

        public IEnumerable<IEvent> GetAll()
        {
            var eventDtos = new List<EventDto>();

            try
            {
                OpenConnection();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = READ_ALL_EVENTS;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        eventDtos.Add(MapReaderToEventDto(reader));
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed(_connection);
            }

            return eventDtos.Select(BuildEventInstanceFromDto)
                                    .Select(@event => _eventMigratorManager.Migrate(@event))
                                    .ToArray();
        }

        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var committedEventStream = new CommittedEventStream(eventSourceId);

            var eventSourceParam = new OracleParameter(EventParameters.EVENTSOURCE, OracleDbType.NVarchar2, 512);
            eventSourceParam.Value = eventSource.GetType().AssemblyQualifiedName;

            var eventSourceIdParam = new OracleParameter(EventParameters.EVENTSOURCEID, OracleDbType.Raw, 16);
            eventSourceIdParam.Value = eventSourceId.ToByteArray();

            var eventDtos = new List<EventDto>();

            try
            {
                OpenConnection();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = READ_STATEMENT_FOR_EVENTS_BY_AGGREGATE_ROOT;
                    command.Parameters.Add(eventSourceIdParam);
                    command.Parameters.Add(eventSourceParam);
                    command.BindByName = true;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        eventDtos.Add(MapReaderToEventDto(reader));
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed(_connection);
            }

            var retrievedEvents = eventDtos.Select(BuildEventInstanceFromDto)
                                    .Select(@event => _eventMigratorManager.Migrate(@event))
                                    .ToList();

            if (retrievedEvents.Any())
                committedEventStream.Append(retrievedEvents);

            return committedEventStream;
        }

        IEvent BuildEventInstanceFromDto(EventDto eventDto)
        {
            var eventType = _eventMigrationHierarchyManager.GetConcreteTypeForLogicalEventMigrationLevel(Type.GetType(eventDto.LogicalName), eventDto.Generation);
            var @event = (IEvent)Activator.CreateInstance(eventType, eventDto.EventSourceId);
            _serializer.FromJson(@event, eventDto.Data, null);
            PopulateGenericEventParametersFromDto(@event, eventDto);
            return @event;
        }

        void PopulateGenericEventParametersFromDto(IEvent @event, EventDto eventDto)
        {
            @event.Id = eventDto.Id;
            @event.Name = eventDto.Name;
            @event.Occured = eventDto.Occurred;
            @event.Origin = eventDto.Origin;
            @event.EventSource = eventDto.EventSource;
            @event.EventSourceId = eventDto.EventSourceId;
            @event.CommandContext = eventDto.CommandContext;
            @event.CausedBy = eventDto.CausedBy;
            @event.Version = EventSourceVersion.FromCombined(eventDto.Version);
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            var eventSourceParam = new OracleParameter(EventParameters.EVENTSOURCE, OracleDbType.NVarchar2, 512);
            eventSourceParam.Value = eventSource.GetType().AssemblyQualifiedName;

            var eventSourceIdParam = new OracleParameter(EventParameters.EVENTSOURCEID, OracleDbType.Raw, 16);
            eventSourceIdParam.Value = eventSourceId.ToByteArray();

            var version = EventSourceVersion.Zero;

            try
            {
                OpenConnection();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = LAST_VERSION_STATEMENT;
                    command.Parameters.Add(eventSourceIdParam);
                    command.Parameters.Add(eventSourceParam);
                    command.BindByName = true;
                    var reader = command.ExecuteReader(CommandBehavior.SingleResult);
                    if (reader.Read())
                    {
                        version = EventSourceVersion.FromCombined(reader.GetDouble(0));
                    }
                }
            }
            finally
            {
                EnsureConnectionClosed(_connection);
            }

            return version;
        }

        string GetParameterizedInsertStatement(int position)
        {
            return string.Format(INSERT_STATEMENT, position);
        }

        EventDto MapReaderToEventDto(OracleDataReader dataReader)
        {
            return new EventDto()
            {
                Id = dataReader.GetInt64(ColumnPositions.Id),
                CommandContext = ConvertToGuid(dataReader.GetOracleBinary(ColumnPositions.CommandContext)),
                Name = dataReader.GetString(ColumnPositions.Name),
                LogicalName = dataReader.GetString(ColumnPositions.LogicalName),
                EventSourceId = ConvertToGuid(dataReader.GetOracleBinary(ColumnPositions.EventSourceId)),
                EventSource = dataReader.GetString(ColumnPositions.EventSource),
                Generation = dataReader.GetInt32(ColumnPositions.Generation),
                Data = dataReader.GetOracleClob(ColumnPositions.Data).Value,
                CausedBy = dataReader.GetString(ColumnPositions.CausedBy),
                Origin = dataReader.GetString(ColumnPositions.Origin),
                Occurred = dataReader.GetDateTime(ColumnPositions.Occured),
                Version = Convert.ToDouble(dataReader.GetDecimal(ColumnPositions.Version))
            };
        }

        void EnsureConnectionClosed(OracleConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                _connection.Close();
        }

        void OpenConnection()
        {
            _connection.Open();
            _connection.EnlistDistributedTransaction(null);
            _connection.EnlistTransaction(null);
        }

        Guid ConvertToGuid(OracleBinary binaryValue)
        {
            return new Guid(binaryValue.Value);
        }

        static class ColumnPositions
        {
            public const int Id = 0;
            public const int CommandContext = 1;
            public const int Name = 2;
            public const int LogicalName = 3;
            public const int EventSourceId = 4;
            public const int EventSource = 5;
            public const int Generation = 6;
            public const int Data = 7;
            public const int CausedBy = 8;
            public const int Origin = 9;
            public const int Occured = 10;
            public const int Version = 11;
        }
    }
}
