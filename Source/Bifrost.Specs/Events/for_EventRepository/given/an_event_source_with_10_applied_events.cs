using System;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Bifrost.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventRepository.given
{
	public class an_event_source_with_10_uncommitted_events_applied
    {
        protected static StatefulAggregatedRoot event_source;
        protected static Guid event_source_id;
        protected static EventRepository event_repository;
        protected static Mock<IEntityContext<IEvent>> entity_context_mock;
        protected static Mock<IEventMigrationHierarchyManager> event_migragtion_hierarchy_manager_mock;

        Establish context = () =>
        {
			entity_context_mock = new Mock<IEntityContext<IEvent>>();
            event_migragtion_hierarchy_manager_mock = new Mock<IEventMigrationHierarchyManager>();
            event_source_id = Guid.NewGuid();
            event_source = new StatefulAggregatedRoot(event_source_id);
            for (var i = 0; i < 10; i++)
            {
                var @event = new SimpleEvent(event_source_id);
                event_source.Apply(@event);
            }
            event_repository = new EventRepository(entity_context_mock.Object, event_migragtion_hierarchy_manager_mock.Object);
        };
    }
}