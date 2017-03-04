using System;
using Bifrost.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_processing_an_event_and_subscriber_has_processed_it : given.an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        static EventSubscription actual_subscription;
        static SomeEvent @event;
        static EventAndEnvelope event_and_envelope;
        static SomeEventSubscriber event_subscriber;

        Establish context = () =>
        {
            subscription.LastEventId = 2;

            var event_envelope = new Mock<IEventEnvelope>();
            event_envelope.SetupGet(e => e.EventId).Returns((long)1);

            var event_source_id = Guid.NewGuid();
            @event = new SomeEvent(event_source_id);
            event_and_envelope = new EventAndEnvelope(event_envelope.Object, new SomeEvent(event_source_id));

            @event = new SomeEvent(Guid.NewGuid());
           
            event_subscriptions_mock.Setup(e => e.Save(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription s) => actual_subscription = s);
            event_subscriber = new SomeEventSubscriber();
            container_mock.Setup(c => c.Get(typeof(SomeEventSubscriber))).Returns(event_subscriber);
            event_subscription_manager = new EventSubscriptionManager(event_subscriptions_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);
        };

        Because of = () => event_subscription_manager.Process(event_and_envelope);

        It should_not_call_subscription_method = () => event_subscriber.ProcessCalled.ShouldBeFalse();
    }
}
