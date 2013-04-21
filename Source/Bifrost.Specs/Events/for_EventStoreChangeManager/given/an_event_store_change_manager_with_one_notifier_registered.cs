using Machine.Specifications;
using Bifrost.Events;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Events.for_EventStoreChangeManager.given
{
    public class an_event_store_change_manager_with_one_notifier_registered : an_event_store_change_notifier
    {
        protected static EventStoreChangeNotifier notifier;

        Establish context = () =>
        {
            notifier = new EventStoreChangeNotifier();
            container_mock.Setup(c => c.Get(typeof(EventStoreChangeNotifier))).Returns(notifier);
            event_store_change_manager.Register(typeof(EventStoreChangeNotifier));
        };
    }
}
