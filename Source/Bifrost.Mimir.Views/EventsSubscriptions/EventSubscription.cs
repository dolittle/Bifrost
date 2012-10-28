using Bifrost.Events;
using Bifrost.Mimir.Events.EventSubscriptions;

namespace Bifrost.Mimir.Views.EventSubscribers
{
    public class EventSubscription : IEventSubscriber
    {
        public void Process(EventSubscriptionEventIdResetted @event)
        {
        }
    }
}
