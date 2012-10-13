using System;
using Bifrost.Commands;

namespace Bifrost.Mimir.Domain.EventSubscribers.Commands
{
    public class ResetEventSubscriberToVersion : Command
    {
        public Guid EventSubscriberId { get; set; }
        public long EventId { get; set; }
    }
}
