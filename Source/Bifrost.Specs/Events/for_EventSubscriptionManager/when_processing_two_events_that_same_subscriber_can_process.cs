using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_processing_two_events_that_same_subscriber_can_process : given.an_event_subscription_manager
    {
        static IEvent[]   events = new IEvent[] {
            new SomeEvent(Guid.NewGuid()) { Id = 1 },
            new SomeOtherEvent(Guid.NewGuid()) { Id = 2 }
        };

        Establish context = () =>
        {
            type_discoverer_mock.Setup(t => t.FindMultiple<IProcessEvents>()).Returns(new[] { typeof(EventSubscriberForTwoEvents) });
            container_mock.Setup(c => c.Get(typeof(EventSubscriberForTwoEvents))).Returns(new EventSubscriberForTwoEvents());
            event_subscription_manager = new EventSubscriptionManager(event_subscription_repository_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);
        };

        Because of = () => event_subscription_manager.Process(events);

        It should_only_create_the_subscriber_once = () => container_mock.Verify(c => c.Get(typeof(EventSubscriberForTwoEvents)), Moq.Times.Once());
    }
}
