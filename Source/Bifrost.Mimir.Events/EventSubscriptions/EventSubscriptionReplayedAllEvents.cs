using System;
using Bifrost.Events;

namespace Bifrost.Mimir.Events.EventSubscriptions
{
    public class EventSubscriptionReplayedAllEvents : Event
    {
        public EventSubscriptionReplayedAllEvents(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public Guid EventSubscriptionId { get; set; }
    }
}
