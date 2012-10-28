using System;
using Bifrost.Commands;

namespace Bifrost.Mimir.Domain.EventSubscriptions.Commands
{
    public class ReplayAllForEventSubscription : Command
    {
        public Guid EventSubscriptionId { get; set; }
    }
}
