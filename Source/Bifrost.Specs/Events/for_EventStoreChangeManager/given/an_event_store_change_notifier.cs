using Machine.Specifications;
using Bifrost.Events;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Events.for_EventStoreChangeManager.given
{
    public class an_event_store_change_notifier
    {
        protected static EventStoreChangeManager event_store_change_manager;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
        {
            container_mock = new Mock<IContainer>();
            event_store_change_manager = new EventStoreChangeManager(container_mock.Object);
        };

    }
}
