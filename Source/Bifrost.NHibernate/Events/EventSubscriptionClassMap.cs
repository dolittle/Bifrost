using Bifrost.Events;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Events
{
    public class EventSubscriptionClassMap : ClassMap<EventSubscription>
    {
        public EventSubscriptionClassMap()
        {
            Table("EventSubscriptions");
            Id(e => e.Id).GeneratedBy.Assigned();
            Map(e => e.Owner);
            Map(e => e.Method);
            Map(e => e.EventType);
            Map(e => e.EventName);
            Map(e => e.LastEventId);
        }
    }
}
