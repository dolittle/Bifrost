using Bifrost.Events;
using Bifrost.Mimir.Events.EventSubscriptions;

namespace Bifrost.Mimir.Views.EventSubscriptions
{
    public class EventSubscribers : IEventSubscriber
    {
        IEventSubscriptionRepository _repository;
        IEventSubscriptionManager _eventSubscriptionManager;
        IEventRepository _eventRepository;

        public EventSubscribers(IEventSubscriptionRepository repository, IEventSubscriptionManager eventSubscriptionManager, IEventRepository eventRepository)
        {
            _repository = repository;
            _eventSubscriptionManager = eventSubscriptionManager;
            _eventRepository = eventRepository;
        }


        public void Process(EventSubscriptionEventIdResetted @event)
        {
        }

        public void Process(EventSubscriptionReplayedAllEvents @event)
        {
            _repository.ResetLastEventId(@event.EventSourceId);
            var subscription = _repository.Get(@event.EventSourceId);
            var events = _eventRepository.GetUnprocessedEventsForSubscriptions(new[] { subscription });
            _eventSubscriptionManager.Process(subscription, events);
        }
    }
}
