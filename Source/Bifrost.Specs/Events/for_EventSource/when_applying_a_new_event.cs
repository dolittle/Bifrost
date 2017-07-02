using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.applying_events)]
	public class when_applying_a_new_event : given.a_stateful_event_source
	{
		Establish context = () => @event = new SimpleEvent(event_source_id);

		Because of = () => event_source.Apply(@event);

		It should_add_the_event_to_the_uncommited_events = () => event_source.UncommittedEvents.ShouldContainOnly(@event);
		It should_increment_the_sequence_of_the_version = () => event_source.Version.Sequence.ShouldEqual(1);
		It should_not_increment_the_commit_of_the_version = () => event_source.Version.Commit.ShouldEqual(0);
	    It should_call_the_on_method_for_the_event = () => event_source.EventApplied.ShouldBeTrue();
	}
}