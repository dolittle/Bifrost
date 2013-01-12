using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;

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
            _documentStore = new Raven.Client.Document.DocumentStore
            {
                Url = _configuration.Url
            };

            if (_configuration.DefaultDatabase != null)
                _documentStore.DefaultDatabase = _configuration.DefaultDatabase;

            if (_configuration.Credentials != null)
                _documentStore.Credentials = _configuration.Credentials;


            var keyGenerator = new SequentialKeyGenerator(_documentStore);
            _documentStore.Conventions.DocumentKeyGenerator = o => string.Format("{0}/{1}", CollectionName, keyGenerator.NextFor<IEvent>());

            _documentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new EventSourceVersionConverter());
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
