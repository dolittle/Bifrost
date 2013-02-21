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
using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Bifrost.RavenDB.Serialization;
using Raven.Client.Connection;

namespace Bifrost.RavenDB.Events
{
    public class EventStore : IEventStore
    {
        const string CollectionName = "Events";
        EventStoreConfiguration _configuration;
        DocumentStore _documentStore;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        public EventStore(EventStoreConfiguration configuration, IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _configuration = configuration;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            InitializeDocumentStore();
            InsertOrModifyEventSourceIdAndVersionIndex();
        }

        void InitializeDocumentStore()
        {
            _documentStore = _configuration.CreateDocumentStore();

            var keyGenerator = new SequentialKeyGenerator(_documentStore);
            _documentStore.Conventions.DocumentKeyGenerator = (a,b,c) => string.Format("{0}/{1}", CollectionName, keyGenerator.NextFor<IEvent>());
            //_documentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter<long>());
            //_documentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter<int>());
            //_documentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter<string>());
            //_documentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter<Guid>());
            //_documentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter<short>());

            _documentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new EventSourceVersionConverter());
                s.Converters.Add(new ConceptConverter());
            };
            _documentStore.Conventions.FindTypeTagName = t => CollectionName;

            _documentStore.RegisterListener(new EventMetaDataListener(_eventMigrationHierarchyManager));

            _documentStore.Initialize();
        }

        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            using (var session = _documentStore.OpenSession())
            {
                var eventSourceType = eventSource.GetType();

                var events = session.Query<IEvent>()
                                    .Where(
                                        e => e.EventSourceId == eventSourceId &&
                                             e.EventSource == eventSourceType.AssemblyQualifiedName
                                        ).ToArray();

                var stream = new CommittedEventStream(eventSourceId);
                stream.Append(events);
                return stream;
            }
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            using (var session = _documentStore.OpenSession())
            {
                var eventArray = uncommittedEventStream.ToArray();
                for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
                {
                    var @event = eventArray[eventIndex];
                    //session.Advanced.
                    session.Store(@event);
                }

                session.SaveChanges();
                var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId);
                committedEventStream.Append(uncommittedEventStream);
                return committedEventStream;
            }
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            using (var session = _documentStore.OpenSession())
            {
                var @event = session.Query<IEvent>()
                                .Where(e => e.EventSourceId == eventSourceId)
                                    .OrderByDescending(e => e.Version)
                                .FirstOrDefault();

                if (@event == null)
                    return EventSourceVersion.Zero;

                return @event.Version;
            }
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            using (var session = _documentStore.OpenSession())
            {
                var events = session.Query<IEvent>().Skip(batchSize * batchesToSkip).Take(batchSize);
                return events.ToArray();
            }
        }

        public IEnumerable<IEvent> GetAll()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<IEvent>().ToArray();
            }
        }


        void InsertOrModifyEventSourceIdAndVersionIndex()
        {
            var alreadyExists = true;
            var updated = false;
            var indexName = "Temp/Events/ByEventSourceIdAndVersionSortByVersion";
            var index = _documentStore.DatabaseCommands.GetIndex(indexName);
            if (index == null)
            {
                index = new IndexDefinition
                {
                    Map = "from doc in docs.Events select new { EventSourceId = doc.EventSourceId, Version = doc.Version }",
                    Fields = new List<string> { "EventSourceId", "Version", "__document_id" }
                };
                alreadyExists = false;
            }

            if (alreadyExists && index.SortOptions.First().Value != SortOptions.Double)
            {
                _documentStore.DatabaseCommands.DeleteIndex(indexName);
                updated = true;
            }

            index.SortOptions = new Dictionary<string, SortOptions> { { "Version", SortOptions.Double } };

            if (!alreadyExists || updated)
                _documentStore.DatabaseCommands.PutIndex(indexName, index);
        }
    }
}
