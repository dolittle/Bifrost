using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Bifrost.Specs.Events.for_EventRepository.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventRepository
{
    [Subject(Subjects.getting_events)]
    public class when_getting_the_events_for_a_specific_event_source : an_event_repository_with_persisted_events
    {
        static IEnumerable<IEvent> returned_events;

        Because of = () =>
                         {
                             returned_events = event_repository.GetForAggregatedRoot(typeof(StatefulAggregatedRoot), event_source_id);
                         };

        It should_return_a_stream_of_10_events = () => returned_events.Count().ShouldEqual(10);
    }
}