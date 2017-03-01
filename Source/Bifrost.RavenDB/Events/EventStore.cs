/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Extensions;
using Bifrost.RavenDB.Serialization;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;

namespace Bifrost.RavenDB.Events
{
    public class EventStore : IEventStore
    {
        const string CollectionName = "Events";
        IEventStoreConfiguration _configuration;
        DocumentStore _documentStore;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;
        IEventEnvelopes _eventEnvelopes;

        public EventStore(IEventStoreConfiguration configuration, IEventMigrationHierarchyManager eventMigrationHierarchyManager, IEventEnvelopes eventEnvelopes)
        {
            _configuration = configuration;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
            _eventEnvelopes = eventEnvelopes;
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
                s.Converters.Add(new MethodInfoConverter());
                s.Converters.Add(new EventSourceVersionConverter());
                s.Converters.Add(new ConceptConverter());
            };
            
           var originalFindTypeTagNam =  _documentStore.Conventions.FindTypeTagName;
           _documentStore.Conventions.FindTypeTagName = t =>
           {
               if (t.HasInterface<IEvent>() || t == typeof(IEvent)) return CollectionName;
               return originalFindTypeTagNam(t);
           };

            _documentStore.RegisterListener(new EventMetaDataListener(_eventMigrationHierarchyManager));
        }

        public CommittedEventStream GetForEventSource(IEventSource eventSource, EventSourceId eventSourceId)
        {
            using (var session = _documentStore.OpenSession())
            {
                var eventSourceType = eventSource.GetType();

                var events = session.Query<IEvent>()
                                    .Where(
                                        e => e.EventSourceId == eventSourceId &&
                                             e.EventSource == eventSourceType.AssemblyQualifiedName
                                        )
                                    .Select(e => new EventEnvelopeAndEvent(_eventEnvelopes.CreateFrom(eventSource, e), e))
                                    .ToArray();

                var stream = new CommittedEventStream(eventSourceId, events);
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
                var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
                return committedEventStream;
            }
        }

        public EventSourceVersion GetLastCommittedVersion(IEventSource eventSource, EventSourceId eventSourceId)
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
