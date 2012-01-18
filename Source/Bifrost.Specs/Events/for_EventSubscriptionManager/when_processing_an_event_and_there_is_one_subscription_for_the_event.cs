using System;
using Bifrost.Events;
using Bifrost.Time;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_processing_an_event_and_there_is_one_subscription_for_the_event : given.an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        static EventSubscription actual_subscription;
        static EventSourceVersion expected_version;
        static SomeEvent @event;
        static SomeEventSubscriber event_subscriber;

        Establish context = () =>
        {
            expected_version = new EventSourceVersion(1, 1);
            @event = new SomeEvent(Guid.NewGuid());
            @event.EventSourceName = EventSourceName;
            @event.Version = expected_version;
            event_subscription_repository_mock.Setup(e => e.Update(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription s) => actual_subscription = s);
            event_subscriber = new SomeEventSubscriber();
            container_mock.Setup(c=>c.Get(typeof(SomeEventSubscriber))).Returns(event_subscriber);
        };

        Because of = () => event_subscription_manager.Process(@event);

        It should_update_subscription = () => actual_subscription.ShouldEqual(subscription);
        It should_update_subscription_with_version = () => actual_subscription.Versions[EventSourceName].ShouldEqual(expected_version);
        It should_call_subscription_method = () => event_subscriber.ProcessCalled.ShouldBeTrue();
    }
}
