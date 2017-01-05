using System;
using Bifrost.NHibernate.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.NHibernate.Specs.Events.for_EventConverter
{
    public class when_converting_to_an_event_holder : given.an_event_converter
    {
        const string caused_by = "Someone";
        const string origin = "Some system";
        static Guid aggregate_id;
        static SimpleEvent @event;
        static EventHolder holder;

        Establish context = () => { 
            aggregate_id = Guid.NewGuid();
            @event = new SimpleEvent(aggregate_id);
            @event.CausedBy = caused_by;
            @event.Origin = origin;
        };

        Because of = () => holder = converter.ToEventHolder(@event);

        It should_have_the_same_aggregate_id = () => holder.AggregateId.ShouldEqual(aggregate_id);
        It should_have_the_same_caused_by = () => holder.CausedBy.ShouldEqual(caused_by);
        It should_have_the_same_origin = () => holder.Origin.ShouldEqual(origin);
        It should_serialize_the_event = () => serializer_mock.Verify(s => s.ToJson(@event, null), Moq.Times.Once());
    }
}
