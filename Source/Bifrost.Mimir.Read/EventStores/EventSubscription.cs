using System;
using Bifrost.Read;

namespace Bifrost.Mimir.Views.EventStores
{
    public class EventSubscription : IReadModel
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string OwnerNamespace { get; set; }
        public string OwnerAssembly { get; set; }
        public string Event { get; set; }
        public long LastEventId { get; set; }
    }
}
