using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.reapplying_events)]
    public class when_reapplying_a_stream_of_committed_events : given.a_stateful_event_source
    {
        static IEvent SecondEvent;
        static IEvent ThirdEvent;
        static CommittedEventStream EventStream;

        Establish context =
            () =>
            {
                @event = new SimpleEvent(event_source_id) { Version = EventSourceVersion.Zero };
                SecondEvent = new SimpleEvent(event_source_id) { Version = @event.Version.NextSequence() };
                ThirdEvent = new SimpleEvent(event_source_id) { Version = @event.Version.NextCommit().NextSequence() };
                EventStream = new CommittedEventStream(event_source_id,new[] {
                    new EventEnvelopeAndEvent(new EventEnvelope(), @event),
                    new EventEnvelopeAndEvent(new EventEnvelope(), SecondEvent),
                    new EventEnvelopeAndEvent(new EventEnvelope(), ThirdEvent),
                });
            };

        Because of = () => event_source.ReApply(EventStream);

        It should_not_add_the_events_to_the_uncommited_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
        It should_increment_the_commit_of_the_version = () => event_source.Version.Commit.ShouldEqual(2);
        It should_being_with_a_sequence_of_zero = () => event_source.Version.Sequence.ShouldEqual(0);
        It should_have_applied_the_event = () => event_source.EventApplied.ShouldBeTrue();
    }
}