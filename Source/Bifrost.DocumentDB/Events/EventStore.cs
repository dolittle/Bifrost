/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bifrost.Events;
using Bifrost.Serialization;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore"/> specifically for the Azure DocumentDB
    /// </summary>
    public class EventStore : IEventStore
    {
        DocumentClient _client;
        Database _database;
        DocumentCollection _collection;
        ISerializer _serializer;
        
        StoredProcedure _insertEventStoredProcedure;
        StoredProcedure _getLastCommittedVersionStoredProcedure;


        /// <summary>
        /// Initializes an instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration">Configuration</see> for storage</param>
        /// <param name="serializer"><see cref="ISerializer">Serializer</see></param>
        public EventStore(EventStorageConfiguration configuration, ISerializer serializer)
        {
            _serializer = serializer;

            Initialize(configuration);
            InitializeCollection();
            
            InitializeStoredProcedures();
        }

        /// <inheritdoc/>
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Commit(IEnumerable<EventAndEnvelope> events)
        {
            foreach( var eventWithEnvelope in events )
            {
                var serialized = _serializer.ToJson(eventWithEnvelope, SerializationExtensions.SerializationOptions);
                _client
                    .ExecuteStoredProcedureAsync<long>(_insertEventStoredProcedure.SelfLink, serialized)
                    .Wait();
            }
        }

        /// <inheritdoc/>
        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            var version = EventSourceVersion.Zero;
            _client
                .ExecuteStoredProcedureAsync<double>(_getLastCommittedVersionStoredProcedure.SelfLink, eventSource.EventSourceId)
                .ContinueWith(t => version = EventSourceVersion.FromCombined(t.Result))
                .Wait();

            return version;
        }


        void InitializeCollection()
        {
            _collection = null;

            var collectionName = "Events";

            _client.ReadDocumentCollectionFeedAsync(_database.SelfLink)
                .ContinueWith(f => _collection = f.Result.Where(c => c.Id == collectionName).SingleOrDefault())
                .Wait();

            if (_collection == null)
            {
                _collection = new DocumentCollection { Id = collectionName };
                _client
                    .CreateDocumentCollectionAsync(_database.SelfLink, _collection)
                    
                    .ContinueWith(r => _collection = r.Result.Resource)
                    .Wait();
            }
        }

        StoredProcedure InitializeStoredProcedureFromResource(string name, string resource)
        {
            StoredProcedure storedProcedure = null;
            var procedure = string.Empty;
            using (var reader = new StreamReader(typeof(EventStore).Assembly.GetManifestResourceStream(resource)))
            {
                procedure = reader.ReadToEnd();
            }

            _client
                .ReadStoredProcedureFeedAsync(_collection.StoredProceduresLink)
                .ContinueWith(t => storedProcedure = t.Result.SingleOrDefault(s => s.Id == name))
                .Wait();

            if (storedProcedure == null)
            {
                storedProcedure = new StoredProcedure
                {
                    Id = name,
                    Body = procedure
                };
                _client
                    .CreateStoredProcedureAsync(_collection.SelfLink, storedProcedure)
                    .ContinueWith(t => storedProcedure = t.Result)
                    .Wait();
            }
            else
            {
                storedProcedure.Body = procedure;
                _client
                    .ReplaceStoredProcedureAsync(storedProcedure)
                    .ContinueWith(t => storedProcedure = t.Result)
                    .Wait();
            }

            return storedProcedure;

        }

        void InitializeStoredProcedures()
        {
            _insertEventStoredProcedure = InitializeStoredProcedureFromResource("insertEvent", "Bifrost.DocumentDB.Events.insertEvent.js");
            _getLastCommittedVersionStoredProcedure = InitializeStoredProcedureFromResource("getLastCommittedVersion", "Bifrost.DocumentDB.Events.getLastCommittedVersion.js");
        }


        void Initialize(EventStorageConfiguration configuration)
        {
            _client = new DocumentClient(new Uri(configuration.Url), configuration.AuthorizationKey);
            _client.ReadDatabaseFeedAsync()
                .ContinueWith(a => _database = a.Result.Where(d => d.Id == configuration.DatabaseId).SingleOrDefault())
                .Wait();

            if (_database == null)
            {
                _database = new Database { Id = configuration.DatabaseId };
                _client.CreateDatabaseAsync(_database)
                    .ContinueWith(d => _database = d.Result.Resource)
                    .Wait();
            }
        }

    }
}
