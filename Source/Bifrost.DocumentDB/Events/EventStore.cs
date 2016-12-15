#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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

#pragma warning disable 1591
        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            throw new NotImplementedException();
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);

            foreach( var @event in uncommittedEventStream )
            {
                var serialized = _serializer.ToJson(@event, SerializationExtensions.CamelCaseOptions);
                _client
                    .ExecuteStoredProcedureAsync<long>(_insertEventStoredProcedure.SelfLink, serialized)
                    .ContinueWith(t => @event.Id = t.Result)
                    .Wait();

                committedEventStream.Append(@event);
            }
            
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            var version = EventSourceVersion.Zero;
            _client
                .ExecuteStoredProcedureAsync<double>(_getLastCommittedVersionStoredProcedure.SelfLink, eventSourceId)
                .ContinueWith(t => version = EventSourceVersion.FromCombined(t.Result))
                .Wait();

            return version;
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetAll()
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1591


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
