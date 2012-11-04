using Bifrost.Events;
using Bifrost.Mimir.Views.EventSubscriptions;
using Machine.Specifications;
using Moq;

namespace Bifrost.Mimir.Views.Specs.EventSubscriptions.for_EventSubscribers.given
{
    public class event_subscribers
    {
        protected static Mock<IEventSubscriptionRepository> event_subscription_repository_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;
        protected static Mock<IEventRepository> event_repository_mock;
        protected static EventSubscribers subscribers;

        Establish context = () =>
        {
            event_subscription_repository_mock = new Mock<IEventSubscriptionRepository>();
            event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();
            event_repository_mock = new Mock<IEventRepository>();
            subscribers = new EventSubscribers(
                    event_subscription_repository_mock.Object,
                    event_subscription_manager_mock.Object,
                    event_repository_mock.Object
                );
        };
    }
}
