using Bifrost.Events;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Events
{
    public class EventClassMap : ClassMap<IEvent>
    {
        public EventClassMap()
        {
            Table("Events");
            Id(e => e.Id).GeneratedBy.Increment();
            
            Map(e => e.CommandContext);
            Map(e => e.CommandName);
            Map(e => e.Name);
            Map(e => e.EventSourceId);
            Map(e => e.AggregatedRoot);
            Map(e => e.EventSourceName);
            Map(e => e.Version).CustomType<EventSourceVersionCustomType>();
            Map(e => e.CausedBy);
            Map(e => e.Origin);
            Map(e => e.Occured);
        }
    }
}
