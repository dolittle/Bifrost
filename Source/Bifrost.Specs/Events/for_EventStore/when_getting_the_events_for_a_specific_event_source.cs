using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStore
{
    [Subject(typeof(EventStore))]
    public class when_getting_the_events_for_a_specific_event_source : given.an_event_store_with_persisted_events
    {
        static IEnumerable<IEvent> returned_events;

        Because of = () =>
                         {
                             returned_events = event_store.GetForEventSource(new StatefulAggregatedRoot(event_source_id), event_source_id);
                         };

        It should_return_a_stream_of_10_events = () => returned_events.Count().ShouldEqual(10);
    }
}