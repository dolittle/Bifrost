using System;
using System.Collections.Generic;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_processing_two_events_that_same_subscriber_can_process : given.an_event_subscription_manager
    {
        static IEnumerable<EventAndEnvelope> events_and_envelopes;

        Establish context = () =>
        {
            var event_source_id = Guid.NewGuid();
            events_and_envelopes = new[]
            {
                new EventAndEnvelope(new Mock<IEventEnvelope>().Object, new SomeEvent(event_source_id)),
                new EventAndEnvelope(new Mock<IEventEnvelope>().Object, new SomeOtherEvent(event_source_id))
            };
            type_discoverer_mock.Setup(t => t.FindMultiple<IProcessEvents>()).Returns(new[] { typeof(EventSubscriberForTwoEvents) });
            container_mock.Setup(c => c.Get(typeof(EventSubscriberForTwoEvents))).Returns(new EventSubscriberForTwoEvents());
            event_subscription_manager = new EventSubscriptionManager(event_subscriptions_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);
        };

        Because of = () => event_subscription_manager.Process(events_and_envelopes);

        It should_only_create_the_subscriber_once = () => container_mock.Verify(c => c.Get(typeof(EventSubscriberForTwoEvents)), Moq.Times.Once());
    }
}
