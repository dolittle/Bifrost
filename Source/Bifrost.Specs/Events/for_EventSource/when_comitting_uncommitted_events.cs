using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.committing_events)]
    public class when_comitting_uncommitted_events : given.an_event_source_with_2_uncommitted_events
    {
        Because of = () => event_source.Commit();

        It should_have_no_uncommitted_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
        It should_increase_the_commit_in_version_by_one = () => event_source.Version.Commit.ShouldEqual(1);
        It should_set_the_sequence_in_version_to_zero = () => event_source.Version.Sequence.ShouldEqual(0);
    }
}
