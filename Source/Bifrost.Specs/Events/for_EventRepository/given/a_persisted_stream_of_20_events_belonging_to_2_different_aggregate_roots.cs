using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Bifrost.Fakes.Events;
using Machine.Specifications;
using Moq;
using Newtonsoft.Json;

namespace Bifrost.Specs.Events.for_EventRepository.given
{
    public class a_persisted_stream_of_20_events_belonging_to_2_different_aggregate_roots
    {
		protected static IQueryable<IEvent> persisted_events;
        protected static Guid event_source_id;
        protected static Guid other_event_source_id;
		protected static Mock<IEntityContext<IEvent>> entity_context_mock;
        protected static Mock<IEventMigrationHierarchyManager> event_migragtion_hierarchy_manager_mock;
        protected static Type aggregate_root_type;
        protected static IEvent last_event_for_event_source;
        protected static IEvent last_event_for_other_event_source;

        Establish context =
            () =>
            {
                event_migragtion_hierarchy_manager_mock = new Mock<IEventMigrationHierarchyManager>();
                aggregate_root_type = typeof(StatefulAggregatedRoot);
                event_source_id = Guid.NewGuid();
                other_event_source_id = Guid.NewGuid();
				var eventEntities = new List<IEvent>();
                for(var i = 0; i < 20; i++)
                {
                    var aggregateId = i%2 == 0 ? event_source_id : other_event_source_id;
                    var @event = new SimpleEvent(aggregateId)
                                     {
                                         EventSource = aggregate_root_type.AssemblyQualifiedName,
                                         Version = new EventSourceVersion(1,i)
                                     };
					var eventEntity = new SimpleEvent(@event.EventSourceId)
                                     {
										 Id = @event.Id,
                                         EventSource = @event.EventSource,
                                         CausedBy = "Some Command",
                                         Name = @event.Name,
                                         Origin = "Somewhere",
                                     };
                    eventEntities.Add(eventEntity);
                }

                persisted_events = eventEntities.AsQueryable();
				entity_context_mock = new Mock<IEntityContext<IEvent>>();
                entity_context_mock.Setup(context => context.Entities).Returns(persisted_events);

                last_event_for_event_source = eventEntities.Where(e => e.EventSourceId == event_source_id).OrderByDescending(e => e.Version).First();
                last_event_for_other_event_source = eventEntities.Where(e => e.EventSourceId == other_event_source_id).OrderByDescending(e => e.Version).First();
            };
    }
}