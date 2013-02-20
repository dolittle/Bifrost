#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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

        EventStoreConfiguration _configuration;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection _collection;
        MongoCollection _incrementalKeysCollection;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        static EventStore()
        {
            BsonSerializer.RegisterSerializer(typeof(EventSourceVersion), new EventSourceVersionSerializer());
        }

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

            if (!_database.CollectionExists(IncrementalKeysCollectionName))
                _database.CreateCollection(IncrementalKeysCollectionName);

            _incrementalKeysCollection = _database.GetCollection(IncrementalKeysCollectionName);
        }

        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var eventSourceType = eventSource.GetType();
            var query = Query.And(
                            Query.EQ("EventSourceId", eventSourceId),
                            Query.EQ("EventSource", eventSourceType.AssemblyQualifiedName)
                        );

            var cursor = _collection.FindAs<BsonDocument>(query);
            var documents = cursor.ToArray();
            var events = ToEvents(documents);
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
                @event.Id = GetNextEventId();
                var eventDocument = @event.ToBsonDocument();
                AddMetaData(@event, eventDocument);
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
            var cursor = _collection.FindAllAs<BsonDocument>();
            cursor.SetSkip(batchSize * batchesToSkip);
            cursor.SetLimit(batchSize);
            var documents = cursor.ToArray();
            var events = ToEvents(documents);
            return events;
        }

        public IEnumerable<IEvent> GetAll()
        {
            var documents = _collection.FindAllAs<BsonDocument>().ToArray();
            var events = ToEvents(documents);
            return events;
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

        IEnumerable<IEvent> ToEvents(IEnumerable<BsonDocument> documents)
        {
            var events = new List<IEvent>();
            
            foreach (var document in documents)
            {
                var eventType = Type.GetType(document[EventType].AsString);
                RemoveMetaData(document);
                var instance = BsonSerializer.Deserialize(document, eventType) as IEvent;
                events.Add(instance);
            }
            return events;
        }
    }
}
