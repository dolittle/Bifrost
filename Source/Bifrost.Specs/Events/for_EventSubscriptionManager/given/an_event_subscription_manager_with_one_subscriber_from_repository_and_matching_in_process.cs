using System;
using Bifrost.Events;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using Bifrost.Time;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager.given
{
    public class an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        protected const string EventSourceName = "MyEventSource";
        protected static EventSubscriptionManager event_subscription_manager;
        protected static Mock<IEventSubscriptionRepository> event_subscription_repository_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;

        protected static EventSubscription subscription;
        protected static Type event_type;
        protected static Type event_subscriber_type;

        Establish context = () =>
        {
            event_subscription_repository_mock = new Mock<IEventSubscriptionRepository>();
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();

            event_type = typeof(SomeEvent);
            event_subscriber_type = typeof(SomeEventSubscriber);
            subscription = new EventSubscription
            {
                EventType = event_type,
                EventName = event_type.Name,
                Owner = event_subscriber_type,
                Method = event_subscriber_type.GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { event_type }),
            };
            subscription.SetEventSourceVersion(EventSourceName, new EventSourceVersion(1, 0));
            event_subscription_repository_mock.Setup(e => e.GetAll()).Returns(new[] { subscription });

            type_discoverer_mock.Setup(t => t.FindMultiple<IEventSubscriber>()).Returns(new[] { typeof(SomeEventSubscriber) });

            event_subscription_manager = new EventSubscriptionManager(event_subscription_repository_mock.Object, type_discoverer_mock.Object, container_mock.Object);
        };
    }

}
