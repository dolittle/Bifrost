using Bifrost.Events;
using FluentNHibernate.Mapping;
using Bifrost.NHibernate.UserTypes;

namespace Bifrost.NHibernate.Events
{
    public class EventSubscriptionClassMap : ClassMap<EventSubscription>
    {
        public EventSubscriptionClassMap()
        {
            Table("EventSubscriptions");
            Id(e => e.Id).GeneratedBy.Assigned();
            Map(e => e.Owner).CustomType<TypeUserType>();
            Map(e => e.Method).CustomType<MethodInfoUserType>();
            Map(e => e.EventType).CustomType<TypeUserType>();
            Map(e => e.EventName);
            Map(e => e.LastEventId);
        }
    }
}
