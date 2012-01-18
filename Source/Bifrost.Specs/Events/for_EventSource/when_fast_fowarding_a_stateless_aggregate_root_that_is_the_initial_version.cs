using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(typeof(EventSource))]
    public class when_fast_fowarding_a_stateless_aggregate_root_that_is_the_initial_version : given.a_stateless_event_source
    {
        static InvalidFastForwardException exception;
        static EventSourceVersion expected_version;
        static EventSourceVersion last_commit;

        Establish context = () =>
                                {
                                    last_commit = new EventSourceVersion(1, 1);
                                    expected_version = last_commit.NextCommit();
                                };

        Because of = () => event_source.FastForward(last_commit);

        It should_fast_forward_the_version = () => event_source.Version.ShouldEqual(expected_version);
    }
}