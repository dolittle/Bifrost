/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
        const string IncrementalKeysCollectionName = "_IncrementalKeys";
        const string EventType = "EventType";
        const string Generation = "Generation";
        const string Version = "Version";
        const string LogicalEventType = "LogicalEventType";
        const string CurrentKey = "CurrentKey";

        EventStorageConfiguration _configuration;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection _collection;
        MongoCollection _incrementalKeysCollection;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;
        IEventEnvelopes _eventEnvelopes;

        static EventStore()
        {
            BsonSerializer.RegisterSerializer(typeof(EventSourceVersion), new EventSourceVersionSerializer());
        }

        public EventStore(EventStorageConfiguration configuration, IEventMigrationHierarchyManager eventMigrationHierarchyManager, IEventEnvelopes eventEnvelopes)
        {
            _configuration = configuration;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            _eventEnvelopes = eventEnvelopes;
            Initialize();
        }

        void Initialize()
        {
            _server = MongoServer.Create(_configuration.Url);
            _database = _server.GetDatabase(_configuration.DefaultDatabase);
            if (!_database.CollectionExists(CollectionName))
                _database.CreateCollection(CollectionName);

            _collection = _database.GetCollection(CollectionName);

            if (!_database.CollectionExists(IncrementalKeysCollectionName))
                _database.CreateCollection(IncrementalKeysCollectionName);

            _incrementalKeysCollection = _database.GetCollection(IncrementalKeysCollectionName);
        }

        public CommittedEventStream GetForEventSource(IEventSource eventSource, EventSourceId eventSourceId)
        {
            var eventSourceType = eventSource.GetType();
            var query = Query.And(
                            Query.EQ("EventSourceId", eventSourceId.Value),
                            Query.EQ("EventSource", eventSourceType.AssemblyQualifiedName)
                        );

            var cursor = _collection.FindAs<BsonDocument>(query);
            var documents = cursor.ToArray();
            var events = ToEvents(documents);
            var stream = new CommittedEventStream(eventSourceId, events);
            
            return stream;
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var eventArray = uncommittedEventStream.ToArray();
            for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
            {
                var eventEnvelopeAndEvent = eventArray[eventIndex];
                eventEnvelopeAndEvent.Event.Id = GetNextEventId();
                var eventDocument = eventEnvelopeAndEvent.Event.ToBsonDocument();
                AddMetaData(eventEnvelopeAndEvent.Event, eventDocument);
                _collection.Insert(eventDocument);
            }

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(IEventSource eventSource, EventSourceId eventSourceId)
        {
            var query = Query.EQ("EventSourceId", eventSourceId.Value);
            var sort = SortBy.Descending(Version);
            var @event = _collection.FindAs<BsonDocument>(query).SetSortOrder(sort).FirstOrDefault();
            if (@event == null)
                return EventSourceVersion.Zero;

            return EventSourceVersion.FromCombined(@event[Version].AsDouble);
        }

        int GetNextEventId()
        {
            var currentValue = 0;
            var query = Query.EQ("_id", CollectionName);
            var result = _incrementalKeysCollection.FindAndModify(query, SortBy.Null, Update.Inc(CurrentKey, 1), true);
            if (result.ModifiedDocument == null)
            {
                var eventsCurrentValue = new BsonDocument();
                eventsCurrentValue["_id"] = CollectionName;
                eventsCurrentValue[CurrentKey] = 1;
                _incrementalKeysCollection.Insert(eventsCurrentValue);
                currentValue = 1;
            }
            else
                currentValue = result.ModifiedDocument[CurrentKey].AsInt32;
            return currentValue;
        }

        void AddMetaData(IEvent @event, BsonDocument eventDocument)
        {
            var eventType = @event.GetType();
            var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
            var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
            eventDocument[EventType] = string.Format("{0}, {1}", eventType.FullName, eventType.Assembly.GetName().Name);
            eventDocument[LogicalEventType] = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
            eventDocument[Generation] = migrationLevel;
        }

        void RemoveMetaData(BsonDocument document)
        {
            document.Remove(EventType);
            document.Remove(LogicalEventType);
            document.Remove(Generation);
        }

        IEnumerable<EventEnvelopeAndEvent> ToEvents(IEnumerable<BsonDocument> documents)
        {
            var events = new List<EventEnvelopeAndEvent>();
            
            foreach (var document in documents)
            {
                var eventType = Type.GetType(document[EventType].AsString);
                RemoveMetaData(document);
                var instance = BsonSerializer.Deserialize(document, eventType) as IEvent;
                events.Add(new EventEnvelopeAndEvent(null, instance));
            }
            return events;
        }
    }
}
