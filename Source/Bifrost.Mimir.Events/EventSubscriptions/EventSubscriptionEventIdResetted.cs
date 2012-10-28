using System;
using Bifrost.Events;

namespace Bifrost.Mimir.Events.EventSubscriptions
{
    public class EventSubscriptionEventIdResetted : Event
    {
        public EventSubscriptionEventIdResetted(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public long EventId { get; set; }
    }
}
