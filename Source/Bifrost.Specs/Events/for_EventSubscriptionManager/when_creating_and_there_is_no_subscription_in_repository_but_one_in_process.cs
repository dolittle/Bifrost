using System;
using Bifrost.Events;
using Bifrost.Execution;
using Machine.Specifications;
using Bifrost.Time;


namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class when_creating_and_there_is_no_subscription_in_repository_but_one_in_process
    {
        protected static EventSubscriptionManager event_subscription_manager;
        protected static Moq.Mock<IEventSubscriptionRepository> event_subscription_repository_mock;
        protected static Moq.Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Moq.Mock<IContainer> container_mock;

        static EventSubscription    expected_subscription;
        static EventSubscription    actual_subscription;

        Establish context = () => 
        {
            event_subscription_repository_mock = new Moq.Mock<IEventSubscriptionRepository>();
            type_discoverer_mock = new Moq.Mock<ITypeDiscoverer>();
            container_mock = new Moq.Mock<IContainer>();

            event_subscription_repository_mock.Setup(s=>s.Add(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription s)=>actual_subscription=s);
            type_discoverer_mock.Setup(s=>s.FindMultiple<IEventSubscriber>()).Returns(new[] { typeof(SomeEventSubscriber)});
            expected_subscription = new EventSubscription 
            {
                Owner = typeof(SomeEventSubscriber),
                Method = typeof(SomeEventSubscriber).GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { typeof(SomeEvent) }),
                EventType = typeof(SomeEvent),
                EventName = typeof(SomeEvent).Name
            };
        };

        Because of = () => event_subscription_manager = new EventSubscriptionManager(event_subscription_repository_mock.Object, type_discoverer_mock.Object, container_mock.Object);

        It should_add_subscription = () => actual_subscription.ShouldEqual(expected_subscription);
    }
}
