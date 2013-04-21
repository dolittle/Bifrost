using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
	[Subject(Subjects.creating_event_source)]
	public class when_creating_a_new_event_source : given.a_stateful_event_source 
	{
		It should_have_a_generated_id = () => event_source.Id.ShouldNotEqual(Guid.Empty);
		It should_not_have_any_uncommitted_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
		It should_have_a_version_of_zero = () => event_source.Version.ShouldEqual(EventSourceVersion.Zero);
	}
}