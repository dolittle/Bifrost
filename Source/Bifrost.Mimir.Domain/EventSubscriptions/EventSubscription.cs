using System;
using Bifrost.Domain;
using Bifrost.Mimir.Events.EventSubscriptions;

namespace Bifrost.Mimir.Domain.EventSubscriptions
{
    public class EventSubscription : AggregatedRoot
    {
        public EventSubscription(Guid id)
            : base(id)
        {
        }

        public void ReplayAll()
        {
            Apply(new EventSubscriptionReplayedAllEvents(Id));
        }

        public void ResetToEventId(long eventId)
        {
        }
    }
}
