using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Bifrost.MongoDB.Events
{
    public class EventStore : IEventStore
    {
        const string CollectionName = "Events";
        const string EventType = "EventType";
        const string Generation = "Generation";
        const string Version = "Version";
        const string LogicalEventType = "LogicalEventType";

        EventStoreConfiguration _configuration;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection _collection;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        public EventStore(EventStoreConfiguration configuration, IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _configuration = configuration;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            Initialize();
        }

        void Initialize()
        {
            _server = MongoServer.Create(_configuration.Url);
            _database = _server.GetDatabase(_configuration.DefaultDatabase);
            if (!_database.CollectionExists(CollectionName))
                _database.CreateCollection(CollectionName);

            _collection = _database.GetCollection(CollectionName);
        }

        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var eventSourceType = eventSource.GetType();

            var events = _collection.FindAllAs<IEvent>().AsQueryable()
                                .Where(
                                    e => e.EventSourceId == eventSourceId &&
                                            e.EventSource == eventSourceType.AssemblyQualifiedName
                                    ).ToArray();

            var stream = new CommittedEventStream(eventSourceId);
            stream.Append(events);
            return stream;
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var eventArray = uncommittedEventStream.ToArray();
            for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
            {
                var @event = eventArray[eventIndex];
                var eventType = @event.GetType();

                var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
                var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);

                var eventDocument = @event.ToBsonDocument();
                eventDocument[EventType] = string.Format("{0}, {1}", eventType.FullName, eventType.Assembly.GetName().Name);
                eventDocument[LogicalEventType] = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
                eventDocument[Generation] = migrationLevel;
                eventDocument[Version] = @event.Version.Combine();
                _collection.Insert(eventDocument);
            }

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
            committedEventStream.Append(uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            var query = Query.EQ("EventSourceId", eventSourceId);
            var sort = SortBy.Descending(Version);
            var @event = _collection.FindAs<BsonDocument>(query).SetSortOrder(sort).FirstOrDefault();
            if (@event == null)
                return EventSourceVersion.Zero;

            return EventSourceVersion.FromCombined(@event[Version].AsDouble);
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            var events = _collection.FindAllAs<IEvent>().AsQueryable().Skip(batchSize * batchesToSkip).Take(batchSize);
            return events.ToArray();
        }
    }
}
