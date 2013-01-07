using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Bifrost.Time;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventRepository
{
    public class when_getting_unprocessed_events_for_one_subscription_and_there_is_one_event_that_is_unprocessed
    {
        const string event_source = "MyEventSource";
        static Mock<IEntityContext<IEvent>>    entity_context_mock;
        static Mock<IEventMigrationHierarchyManager> event_migration_hierarchy_manager_mock;
        static EventRepository repository;
        static EventSubscription subscription;
        static IEnumerable<IEvent> result;
        static SimpleEvent expected_event;
        static IEvent @event;

        Establish context = ()=> 
        {
            subscription = new EventSubscription
            {
                Owner = typeof(Subscribers),
                Method = typeof(Subscribers).GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { typeof(SimpleEvent) }),
                EventType = typeof(SimpleEvent),
                EventName = typeof(SimpleEvent).Name,
                LastEventId = 0
            };

            expected_event = new SimpleEvent(Guid.NewGuid());
            expected_event.EventSource = event_source;
            expected_event.Occured = DateTime.Now;

            entity_context_mock = new Mock<IEntityContext<IEvent>>();

            @event = new SimpleEvent(expected_event.EventSourceId)
            {
                Name = expected_event.GetType().Name,
                Occured = expected_event.Occured
            };

            entity_context_mock.Setup(e => e.Entities).Returns(new[] { @event }.AsQueryable());

            event_migration_hierarchy_manager_mock = new Mock<IEventMigrationHierarchyManager>();

            event_migration_hierarchy_manager_mock.Setup(e => e.GetLogicalTypeFromName(subscription.EventName)).Returns(expected_event.GetType());

            repository = new EventRepository(entity_context_mock.Object, event_migration_hierarchy_manager_mock.Object);

        };

        Because of = () => result = repository.GetUnprocessedEventsForSubscriptions(new[] { subscription });

        It should_return_one_event = () => result.Count().ShouldEqual(1);
    }
}
