using System;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Globalization;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_creating_and_there_is_no_subscription_in_repository_but_one_in_process
    {
        protected static EventSubscriptionManager event_subscription_manager;
        protected static Mock<IEventSubscriptions> event_subscriptions_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ILocalizer> localizer_mock;

        static EventSubscription    expected_subscription;
        static EventSubscription    actual_subscription;

        Establish context = () => 
        {
            event_subscriptions_mock = new Moq.Mock<IEventSubscriptions>();
            type_discoverer_mock = new Moq.Mock<ITypeDiscoverer>();
            container_mock = new Moq.Mock<IContainer>();

            localizer_mock = new Mock<ILocalizer>();

            event_subscriptions_mock.Setup(s=>s.Save(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription s)=>actual_subscription=s);
            type_discoverer_mock.Setup(s=>s.FindMultiple<IProcessEvents>()).Returns(new[] { typeof(SomeEventSubscriber)});
            expected_subscription = new EventSubscription 
            {
                Owner = typeof(SomeEventSubscriber),
                Method = typeof(SomeEventSubscriber).GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { typeof(SomeEvent) }),
                EventType = typeof(SomeEvent),
                EventName = typeof(SomeEvent).Name
            };
        };

        Because of = () => event_subscription_manager = new EventSubscriptionManager(event_subscriptions_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);

        It should_add_subscription = () => actual_subscription.ShouldEqual(expected_subscription);
        It should_assign_a_new_id = () => actual_subscription.Id.ShouldNotEqual(Guid.Empty);
    }
}
