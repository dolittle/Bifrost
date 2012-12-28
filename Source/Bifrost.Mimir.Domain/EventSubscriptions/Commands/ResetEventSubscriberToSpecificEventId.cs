using System;
using Bifrost.Commands;

namespace Bifrost.Mimir.Domain.EventSubscriptions.Commands
{
    public class ResetEventSubscriberToSpecificEventId : Command
    {
        public Guid EventSubscriptionId { get; set; }
        public long EventId { get; set; }
    }
}
