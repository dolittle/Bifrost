using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventStoreChangeManager
{
    public class EventStoreChangeNotifier : IEventStoreChangeNotifier
    {
        public bool NotifyCalled = false;
        public IEventStore EventStore = null;
        public void Notify(IEventStore eventStore)
        {
            NotifyCalled = true;
            EventStore = eventStore;
        }
    }
}
