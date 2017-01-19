using Bifrost.Events;
using Machine.Specifications;
using System.Linq;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_loading_events_for_a_specific_aggregated_root_and_saga_has_events_from_multiple_aggregatedroots : given.a_saga_with_events_from_multiple_aggregated_roots
    {
        static CommittedEventStream event_stream;

        Because of = () => event_stream = saga.GetForEventSource(Moq.It.IsAny<EventSource>(), second_aggregated_root_id);

        It should_return_only_one_event = () => event_stream.Count().ShouldEqual(1);
        It should_return_only_the_events_for_the_specific_aggregated_root = () => event_stream.First().EventSourceId.ShouldEqual(second_aggregated_root_id);
    }
}
