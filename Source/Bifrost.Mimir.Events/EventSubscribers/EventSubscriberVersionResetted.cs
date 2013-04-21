using System;
using Bifrost.Events;

namespace Bifrost.Mimir.Events.EventSubscribers
{
    public class EventSubscriberVersionResetted : Event
    {
        public EventSubscriberVersionResetted(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public long EventId { get; set; }
    }
}
