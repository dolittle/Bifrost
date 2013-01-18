using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bifrost.Events;
using Bifrost.Serialization;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Bifrost.Oracle.Events
{
    public class EventStore : IEventStore
    {
        const string INSERT_STATEMENT =
            "INSERT INTO EVENTS (COMMANDCONTEXT,NAME,LOGICALNAME,EVENTSOURCEID,EVENTSOURCE,GENERATION,DATA,CAUSEDBY,ORIGIN,OCCURED,VERSION)"
            + " VALUES(:COMMANDCONTEXT_{0},:NAME_{0},:LOGICALNAME_{0},:EVENTSOURCEID_{0},:EVENTSOURCE_{0},:GENERATION_{0},:DATA_{0},:CAUSEDBY_{0},:ORIGIN_{0},:OCCURED_{0},:VERSION_{0})"
            + " RETURNING ID INTO :ID_{0};";

        const string READ_STATEMENT =
            "SELECT ID, COMMANDCONTEXT, NAME, LOGICALNAME, EVENTSOURCEID, EVENTSOURCE, GENERATION, DATA, CAUSEDBY, ORIGIN, OCCURED, VERSION" +
            " FROM EVENTS" +
            " WHERE EVENTSOURCE = :EVENTSOURCE AND EVENTSOURCEID = :EVENTSOURCEID";

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
            throw new System.NotImplementedException();
        }

        public IEnumerable<IEvent> GetAll()
        {
            throw new System.NotImplementedException();
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
                    command.CommandText = READ_STATEMENT;
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

    public class EventParameter
    {
        readonly string _name;
        readonly OracleDbType _dbType;
        readonly int _size;

        protected string Name { get { return _name; } }
        protected OracleDbType DbType { get { return _dbType; } }
        protected int Size { get { return _size; } }

        public EventParameter(string name, OracleDbType dbType, int size)
        {
            _name = name;
            _dbType = dbType;
            _size = size;
        }

        public EventParameter(string name, OracleDbType dbType)
            : this(name, dbType, 0)
        {
        }

        public virtual OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            var param = _size > 0 ? new OracleParameter(GetFormattedParameterName(position), _dbType, _size) : new OracleParameter(GetFormattedParameterName(position), _dbType);
            param.Value = GetValue(propertiesOnEvent, @event, metaData);
            return param;
        }

        protected virtual object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            var property = propertiesOnEvent.Single(pi => pi.Name.Equals(_name, StringComparison.InvariantCultureIgnoreCase));
            var value = property.GetValue(@event, null);

            if (value is Guid)
                return ((Guid)value).ToByteArray();

            if (value is EventSourceVersion)
                return ((EventSourceVersion)value).Combine();

            return value;
        }

        protected string GetFormattedParameterName(int position)
        {
            return string.Concat(_name, "_", position);
        }
    }

    public class IdEventParameter : EventParameter
    {
        public IdEventParameter()
            : base(EventParameters.ID, OracleDbType.Int64, 10)
        {
        }

        public override OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            return new OracleParameter(GetFormattedParameterName(position), DbType, ParameterDirection.Output);
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return null;
        }
    }

    public class MetaDataEventParameter : EventParameter
    {
        readonly Func<EventMetaData, object> _valueFromMetaData;

        public MetaDataEventParameter(string name, OracleDbType dbType, int size, Func<EventMetaData, object> valueFromMetaData)
            : base(name, dbType, size)
        {
            _valueFromMetaData = valueFromMetaData;
        }

        public MetaDataEventParameter(string name, OracleDbType dbType, Func<EventMetaData, object> valueFromMetaData)
            : this(name, dbType, 0, valueFromMetaData)
        {
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return _valueFromMetaData.Invoke(metaData);
        }
    }

    public class DataEventParameter : EventParameter
    {
        readonly Func<IEvent, IEnumerable<PropertyInfo>> _getDataProperties;
        readonly Func<dynamic, string> _serializer;

        public DataEventParameter(Func<IEvent, IEnumerable<PropertyInfo>> getDataProperties, Func<dynamic, string> serializer)
            : base(EventParameters.DATA, OracleDbType.NClob, 0)
        {
            _getDataProperties = getDataProperties;
            _serializer = serializer;
        }

        public override OracleParameter BuildParameter(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData, int position)
        {
            var value = GetValue(propertiesOnEvent, @event, metaData) as string;

            var param = new OracleParameter(GetFormattedParameterName(position), DbType);
            param.Size = value.Length;
            param.Value = value;
            return param;
        }

        protected override object GetValue(PropertyInfo[] propertiesOnEvent, IEvent @event, EventMetaData metaData)
        {
            return SerializeDataPropertiesToString(_getDataProperties.Invoke(@event), @event);
        }

        string SerializeDataPropertiesToString(IEnumerable<PropertyInfo> properties, IEvent @event)
        {
            var eventData = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)eventData;
            foreach (var property in properties)
            {
                dictionary[property.Name] = property.GetValue(@event, null);
            }
            return _serializer.Invoke(eventData);
        }
    }

    public class EventMetaData
    {
        public string LogicalName { get; set; }
        public int Generation { get; set; }
    }

    public class EventDto
    {
        public long Id { get; set; }
        public Guid CommandContext { get; set; }
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public Guid EventSourceId { get; set; }
        public string EventSource { get; set; }
        public int Generation { get; set; }
        public string Data { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
        public DateTime Occurred { get; set; }
        public double Version { get; set; }
    }

    public class EventParameters : IEventParameters
    {
        public const string ID = "ID";
        public const string COMMANDCONTEXT = "COMMANDCONTEXT";
        public const string NAME = "NAME";
        public const string LOGICALNAME = "LOGICALNAME";
        public const string EVENTSOURCEID = "EVENTSOURCEID";
        public const string EVENTSOURCE = "EVENTSOURCE";
        public const string GENERATION = "GENERATION";
        public const string DATA = "DATA";
        public const string CAUSEDBY = "CAUSEDBY";
        public const string ORIGIN = "ORIGIN";
        public const string OCCURED = "OCCURED";
        public const string VERSION = "VERSION";

        readonly ISerializer _serializer;
        readonly IEventMigrationHierarchyManager _eventMigrationHierarchyManager;
        readonly PropertyInfo[] _eventProperties;
        readonly Dictionary<Type, PropertyInfo[]> _cachedEventProperties = new Dictionary<Type, PropertyInfo[]>();
        readonly Dictionary<Type, EventMetaData> _cachedEventMetaData = new Dictionary<Type, EventMetaData>();

        public EventParameters(ISerializer serializer, IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _serializer = serializer;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            _eventProperties = typeof(IEvent).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Parameters = new[]
            {
                new EventParameter(COMMANDCONTEXT,OracleDbType.Raw,16), 
                new EventParameter(NAME,OracleDbType.NVarchar2,512),
                new MetaDataEventParameter(LOGICALNAME, OracleDbType.NVarchar2, 512, m => m.LogicalName), 
                new EventParameter(EVENTSOURCEID,OracleDbType.Raw,16), 
                new EventParameter(EVENTSOURCE,OracleDbType.NVarchar2,512),
                new MetaDataEventParameter(GENERATION, OracleDbType.Int32, m => m.Generation),
                new DataEventParameter(GetDataProperties, d => _serializer.ToJson(d,null)), 
                new EventParameter(CAUSEDBY,OracleDbType.NVarchar2,512), 
                new EventParameter(ORIGIN,OracleDbType.NVarchar2,512), 
                new EventParameter(OCCURED,OracleDbType.Date),
                new EventParameter(VERSION,OracleDbType.Double), 
                new IdEventParameter()
            };
        }

        public EventParameter[] Parameters { get; private set; }

        IEnumerable<PropertyInfo> GetDataProperties(IEvent @event)
        {
            var eventType = @event.GetType();
            if (!_cachedEventProperties.Keys.Contains(eventType))
            {
                _cachedEventProperties.Add(eventType,
                                           eventType.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                                   BindingFlags.DeclaredOnly));
            }

            return _cachedEventProperties[eventType];
        }

        public EventMetaData GetMetaDataFor(IEvent @event)
        {
            var eventType = @event.GetType();
            if (!_cachedEventMetaData.Keys.Contains(eventType))
            {
                var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
                var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
                var name = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
                _cachedEventMetaData.Add(eventType, new EventMetaData { Generation = migrationLevel, LogicalName = name });
            }

            return _cachedEventMetaData[eventType];
        }

        public IEnumerable<OracleParameter> BuildFromEvent(int position, IEvent @event)
        {
            return Parameters.Select(parameter => parameter.BuildParameter(_eventProperties, @event, GetMetaDataFor(@event), position));
        }
    }

    public interface IEventParameters
    {
        EventParameter[] Parameters { get; }
        EventMetaData GetMetaDataFor(IEvent @event);
        IEnumerable<OracleParameter> BuildFromEvent(int position, IEvent @event);
    }

    public class EventStoreConfiguration
    {
        public OracleConnection Connection { get; set; }
    }
}
