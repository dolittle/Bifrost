using System;
using Bifrost.Events;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using Bifrost.Time;
using Bifrost.Globalization;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager.given
{
    public class an_event_subscription_manager_with_one_subscriber_from_repository_and_matching_in_process
    {
        protected const string event_source = "MyEventSource";
        protected static EventSubscriptionManager event_subscription_manager;
        protected static Mock<IEventSubscriptions> event_subscriptions_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ILocalizer> localizer_mock;

        protected static EventSubscription subscription;
        protected static Type event_type;
        protected static Type event_subscriber_type;


        Establish context = () =>
        {
            event_subscriptions_mock = new Mock<IEventSubscriptions>();
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();

            event_type = typeof(SomeEvent);
            event_subscriber_type = typeof(SomeEventSubscriber);
            subscription = new EventSubscription
            {
                Id = 42,
                EventType = event_type,
                EventName = event_type.Name,
                Owner = event_subscriber_type,
                Method = event_subscriber_type.GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { event_type }),
                LastEventId = 0
            };
            event_subscriptions_mock.Setup(e => e.GetAll()).Returns(new[] { subscription });

            type_discoverer_mock.Setup(t => t.FindMultiple<IProcessEvents>()).Returns(new[] { typeof(SomeEventSubscriber) });

            localizer_mock = new Mock<ILocalizer>();
            event_subscription_manager = new EventSubscriptionManager(event_subscriptions_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);
        };
    }

}
