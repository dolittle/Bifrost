using Bifrost.Events;
using Bifrost.Mimir.Events.EventStores;

namespace Bifrost.Mimir.Views.EventStores
{
    public class EventSubscribers : IEventSubscriber
    {
        IEventSubscriptionRepository _eventSubscriptionRepository;

        public EventSubscribers(IEventSubscriptionRepository eventSubscriptionRepository)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
        }

        public void Process(AllEventsReplayed @event)
        {
            _eventSubscriptionRepository.ResetLastEventForAllSubscriptions();
        }
    }
}
