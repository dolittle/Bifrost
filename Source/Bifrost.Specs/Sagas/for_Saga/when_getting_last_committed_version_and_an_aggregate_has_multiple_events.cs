using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_last_committed_version_and_an_aggregate_has_multiple_events : given.a_saga_with_an_aggregated_root_with_multiple_events
    {
        static EventSourceVersion version;

        Because of = () => version = saga.GetLastCommittedVersion(Moq.It.IsAny<EventSource>(), aggregated_root_id);

        It should_return_the_right_version = () => version.ShouldEqual(second_event.Version);
    }
}
