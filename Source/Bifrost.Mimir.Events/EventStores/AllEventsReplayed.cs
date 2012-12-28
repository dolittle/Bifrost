using System;
using Bifrost.Events;

namespace Bifrost.Mimir.Events.EventStores
{
    public class AllEventsReplayed : Event
    {
        public AllEventsReplayed(Guid eventSourceId)
            : base(eventSourceId)
        {
        }
    }
}
