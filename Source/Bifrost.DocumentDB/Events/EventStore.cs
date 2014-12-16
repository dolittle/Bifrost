#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using Bifrost.JSON.Serialization;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

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
        JsonSerializer _serializer;


        /// <summary>
        /// Initializes an instance of <see cref="EventStore"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration">Configuration</see> for storage</param>
        public EventStore(EventStorageConfiguration configuration)
        {
            Initialize(configuration);
            InitializeCollection();
            InitializeSerializer();
        }

#pragma warning disable 1591
        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            throw new NotImplementedException();
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            foreach( var @event in uncommittedEventStream )
            {
                using( var stream = SerializeToStream(@event))
                {
                    _client.CreateDocumentAsync(_collection.DocumentsLink, Resource.LoadFrom<Document>(stream));
                }
            }

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
            committedEventStream.Append(uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            throw new NotImplementedException();
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

        Stream SerializeToStream(IEvent @event)
        {
            var serialized = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                _serializer.Serialize(stringWriter, @event);
                serialized = stringWriter.ToString();
            }

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(serialized);
                writer.Flush();
                stream.Position = 0;
            }

            return stream;
        }

        void InitializeSerializer()
        {
            _serializer = new JsonSerializer();
            _serializer.Converters.Add(new MethodInfoConverter());
            _serializer.Converters.Add(new ConceptConverter());
            _serializer.Converters.Add(new ConceptDictionaryConverter());
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
