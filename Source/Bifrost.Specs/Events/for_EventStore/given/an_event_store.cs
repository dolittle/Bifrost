using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventStore.given
{
    public class an_event_store : Globalization.given.a_localizer_mock
    {
        protected static EventStore event_store;
        protected static Mock<IEventStoreChangeManager> event_store_change_manager_mock;
        protected static Mock<IEventRepository> event_repository_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;

        Establish context = () =>
        {
            event_repository_mock = new Mock<IEventRepository>();
            event_store_change_manager_mock = new Mock<IEventStoreChangeManager>();
            event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();
            event_store = new EventStore(event_repository_mock.Object, event_store_change_manager_mock.Object, event_subscription_manager_mock.Object, localizer_mock.Object);
        };
    }
}
