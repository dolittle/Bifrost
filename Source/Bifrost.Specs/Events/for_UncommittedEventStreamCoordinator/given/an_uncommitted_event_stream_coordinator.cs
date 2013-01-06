using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator.given
{
    public class an_uncommitted_event_stream_coordinator
    {
        protected static UncommittedEventStreamCoordinator coordinator;
        protected static Mock<IEventStoreChangeManager> event_store_change_manager_mock;
        protected static Mock<IEventStore> event_store_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;

        Establish context = () =>
        {
            event_store_mock = new Mock<IEventStore>();
            event_store_change_manager_mock = new Mock<IEventStoreChangeManager>();
            event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();
            coordinator = new UncommittedEventStreamCoordinator(event_store_mock.Object, event_store_change_manager_mock.Object, event_subscription_manager_mock.Object);
        };
    }
}
