using System;
using Bifrost.Domain;

namespace Bifrost.Mimir.Domain.EventSubscribers
{
    public class EventSubscriber : AggregatedRoot
    {
        public EventSubscriber(Guid id)
            : base(id)
        {
        }
    }
}
