using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventStoreChangeManager
{
    public class EventStoreChangeNotifier : IEventStoreChangeNotifier
    {
        public bool NotifyCalled = false;
        public IEventStore EventStore = null;
        public EventStream EventStream = null;

        public void Notify(IEventStore eventStore, EventStream streamOfEvents)
        {
            NotifyCalled = true;
            EventStore = eventStore;
            EventStream = streamOfEvents;
        }
    }
}
