using Bifrost.Events;
using Bifrost.Mimir.Events.EventStores;
using Bifrost.Tasks;

namespace Bifrost.Mimir.Views.EventStores
{
    public class EventSubscribers : IEventSubscriber
    {
        IEventSubscriptionRepository _eventSubscriptionRepository;
        ITaskManager _taskManager;

        public EventSubscribers(IEventSubscriptionRepository eventSubscriptionRepository, ITaskManager taskManager)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
            _taskManager = taskManager;
        }

        public void Process(AllEventsReplayed @event)
        {
            _eventSubscriptionRepository.ResetLastEventForAllSubscriptions();
            _taskManager.Start<ResetAllEventsForAllSubscriptionsTask>();
        }
    }
}
