using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_last_committed_version_and_an_aggregate_has_multiple_events : given.a_saga_and_an_event_source_and_multiple_committed_events
    {
        static EventSourceVersion version;

        Because of = () => version = saga.GetLastCommittedVersionFor(event_source.Object);

        It should_return_the_right_version = () => version.ShouldEqual(second_event_envelope.Object.Version);
    }
}
