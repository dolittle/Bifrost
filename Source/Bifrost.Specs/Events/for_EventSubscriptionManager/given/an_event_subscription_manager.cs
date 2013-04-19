using Bifrost.Events;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using Bifrost.Globalization;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager.given
{
    public class an_event_subscription_manager
    {
        protected static EventSubscriptionManager event_subscription_manager;
        protected static Mock<IEventSubscriptionRepository> event_subscription_repository_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ILocalizer> localizer_mock;

        Establish context = () =>
        {
            event_subscription_repository_mock = new Mock<IEventSubscriptionRepository>();
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
            localizer_mock = new Mock<ILocalizer>();
            event_subscription_manager = new EventSubscriptionManager(event_subscription_repository_mock.Object, type_discoverer_mock.Object, container_mock.Object, localizer_mock.Object);
        };
    }
}
