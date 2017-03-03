using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.rolling_back)]
    public class when_rolling_back_uncommitted_events : given.an_event_source_with_2_uncommitted_events
    {
        Because of = () => event_source.Rollback();

        It should_have_no_uncommitted_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
        It should_have_sequence_in_version_set_to_zero = () => event_source.Version.Sequence.ShouldEqual(0);
    }
}