using System;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource.given
{
    [Subject(typeof(EventSourceExtensions))]
    public class two_different_event_source_types_that_handle_different_events
    {
        protected static StatefulAggregatedRoot event_source;
        protected static AnotherStatefulAggregatedRoot second_event_source;
        protected static Guid event_source_id;
        protected static Guid second_event_source_id;
        protected static SimpleEvent simple_event;
        protected static AnotherSimpleEvent another_simple_event;
        
        Establish context = () =>
                                {
                                    event_source_id = Guid.NewGuid();
                                    second_event_source_id = Guid.NewGuid();
                                    simple_event = new SimpleEvent(event_source_id);
                                    another_simple_event = new AnotherSimpleEvent(second_event_source_id);

                                    event_source = new StatefulAggregatedRoot(event_source_id);
                                    second_event_source = new AnotherStatefulAggregatedRoot(second_event_source_id);
                                };
    }
}