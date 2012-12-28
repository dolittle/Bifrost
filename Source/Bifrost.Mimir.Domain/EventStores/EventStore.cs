using System;
using Bifrost.Domain;
using Bifrost.Mimir.Events.EventStores;

namespace Bifrost.Mimir.Domain.EventStores
{
    public class EventStore : AggregatedRoot
    {
        public static readonly Guid SystemEventStoreId = new Guid("5E3FD0AD-FF67-43A6-933D-E5B9B68AF5C3");

        public EventStore(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public void ReplayAll()
        {
            Apply(new AllEventsReplayed(Id));
        }
    }
}
