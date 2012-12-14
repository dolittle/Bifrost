using Bifrost.Events;
using Bifrost.Mimir.Events.EventSubscribers;

namespace Bifrost.Mimir.Views.EventSubscribers
{
    public class EventSubscribers : IEventSubscriber
    {
        public void Process(EventSubscriberVersionResetted @event)
        {
        }
    }
}
