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

namespace Bifrost.MongoDb.Events
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
		MongoClient _server;
		IMongoDatabase _database;
		IMongoCollection<BsonDocument> _collection;
		IMongoCollection<BsonDocument> _incrementalKeysCollection;
		IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

		static EventStore()
		{
			BsonSerializer.RegisterSerializer(typeof(EventSourceVersion), new EventSourceVersionSerializer());
		}

		public EventStore(EventStorageConfiguration configuration, IEventMigrationHierarchyManager eventMigrationHierarchyManager)
		{
			_configuration = configuration;
			_eventMigrationHierarchyManager = eventMigrationHierarchyManager;
			Initialize();
		}

		void Initialize()
		{
			var s = MongoClientSettings.FromUrl(new MongoUrl(_configuration.Url));
			if (_configuration.UseSSL)
			{
				s.UseSsl = true;
				s.SslSettings = new SslSettings
				{
					EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
					CheckCertificateRevocation = false
				};
			}
			_server = new MongoClient(s);


			_database = _server.GetDatabase(_configuration.DefaultDatabase);

			_collection = _database.GetCollection<BsonDocument>(CollectionName);

			_incrementalKeysCollection = _database.GetCollection<BsonDocument>(IncrementalKeysCollectionName);
		}

		public CommittedEventStream GetForEventSource(IEventSource eventSource, EventSourceId eventSourceId)
		{
			var eventSourceType = eventSource.GetType();
			var builder = Builders<BsonDocument>.Filter;
			var filter = builder.Eq("EventSourceId", eventSourceId) & builder.Eq("EventSource", eventSourceType.AssemblyQualifiedName);

			var cursor = _collection.Find<BsonDocument>(filter);
			var documents = cursor.ToList();
			var events = ToEvents(documents);
			var stream = new CommittedEventStream(eventSourceId, events);
			return stream;
		}

		public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
		{
			var eventArray = uncommittedEventStream.ToArray();
			for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
			{
				var @event = eventArray[eventIndex];
				@event.Event.Id = GetNextEventId();
				var eventDocument = @event.ToBsonDocument();
				AddMetaData(@event.Event, eventDocument);
				_collection.InsertOne(eventDocument);
			}

			var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, eventArray);
			return committedEventStream;
		}

		public EventSourceVersion GetLastCommittedVersion(IEventSource eventSource, EventSourceId eventSourceId)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("EventSourceId", eventSourceId);
			var @event = _collection.Find<BsonDocument>(filter).SortBy(d => d.GetElement(Version)).FirstOrDefault();
			if (@event == null)
				return EventSourceVersion.Zero;

			return EventSourceVersion.FromCombined(@event[Version].AsDouble);
		}

		int GetNextEventId()
		{
			var currentValue = 0;
			var query = Builders<BsonDocument>.Filter.Eq("_id", CollectionName);
			var result = _incrementalKeysCollection.FindOneAndUpdate(query, Builders<BsonDocument>.Update.Inc(CurrentKey, 1), new FindOneAndUpdateOptions<BsonDocument>() { ReturnDocument = ReturnDocument.After, IsUpsert = true });

			//TODO: Is this really necessary? The above is an upsert?
			if (result?.AsBsonDocument == null)
			{
				var eventsCurrentValue = new BsonDocument();
				eventsCurrentValue["_id"] = CollectionName;
				eventsCurrentValue[CurrentKey] = 1;
				_incrementalKeysCollection.InsertOne(eventsCurrentValue);
				currentValue = 1;
			}
			else
				currentValue = result.AsBsonDocument[CurrentKey].AsInt32;
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
