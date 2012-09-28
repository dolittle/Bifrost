using Bifrost.Events;

namespace Bifrost.SignalR.Events
{
    public class EventStoreChangeNotifier : IEventStoreChangeNotifier
    {
        public void Notify(IEventStore eventStore, EventStream streamOfEvents)
        {
            
        }
    }
}
