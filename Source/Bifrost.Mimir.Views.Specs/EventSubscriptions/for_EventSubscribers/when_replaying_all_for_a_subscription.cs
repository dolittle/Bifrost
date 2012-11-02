using System;
using Bifrost.Mimir.Events.EventSubscriptions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using System.Collections.Generic;

namespace Bifrost.Mimir.Views.Specs.EventSubscriptions.for_EventSubscribers
{
    public class when_replaying_all_for_a_subscription : given.event_subscribers
    {
        static EventSubscription subscription;
        static Guid event_source_id = Guid.NewGuid();
        static Guid event_subscription_id = Guid.NewGuid();

        Establish context = () =>
        {
            subscription = new EventSubscription
            {
                Id = event_subscription_id,
                EventName = "SimpleEvent",
                EventType = typeof(SimpleEvent),
                Owner = typeof(Subscribers),
                Method = typeof(Subscribers).GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { typeof(SimpleEvent) })
            };
            event_subscription_repository_mock.Setup(e => e.Get(event_subscription_id)).Returns(subscription);
        };

        Because of = () => subscribers.Process(new EventSubscriptionReplayedAllEvents(event_source_id) { EventSourceId = event_subscription_id });

        It should_reset_last_event_on_subscriber = () => event_subscription_repository_mock.Verify(e => e.ResetLastEventId(event_subscription_id), Times.Once());
        It should_get_subscription = () => event_subscription_repository_mock.Verify(e => e.Get(event_subscription_id), Times.Once());
        It should_get_unprocessed_events_for_subscription = () => event_repository_mock.Verify(e => e.GetUnprocessedEventsForSubscriptions(new[] { subscription }), Times.Once());
        It should_process_all_events = () => event_subscription_manager_mock.Verify(e=>e.Process(subscription, Moq.It.IsAny<IEnumerable<IEvent>>()), Times.Once());
    }
}
