using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSource
{
    [Subject(Subjects.reapplying_events)]
    public class when_reapplying_a_stream_of_committed_events : given.a_stateful_event_source
    {
        static IEvent second_event;
        static Mock<IEventEnvelope> second_event_envelope;
        static IEvent third_event;
        static Mock<IEventEnvelope> third_event_envelope;
        static CommittedEventStream event_stream;

        Establish context =
            () =>
            {
                @event = new SimpleEvent(event_source_id);
                event_envelope = new Mock<IEventEnvelope>();
                event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero);

                second_event = new SimpleEvent(event_source_id);
                second_event_envelope = new Mock<IEventEnvelope>();
                second_event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero.NextSequence());

                third_event = new SimpleEvent(event_source_id);
                third_event_envelope = new Mock<IEventEnvelope>();
                third_event_envelope.SetupGet(e => e.Version).Returns(EventSourceVersion.Zero.NextCommit().NextSequence());

                event_stream = new CommittedEventStream(event_source_id,new[] {
                    new EventAndEnvelope(event_envelope.Object, @event),
                    new EventAndEnvelope(second_event_envelope.Object, second_event),
                    new EventAndEnvelope(third_event_envelope.Object, third_event),
                });
            };

        Because of = () => event_source.ReApply(event_stream);

        It should_not_add_the_events_to_the_uncommited_events = () => event_source.UncommittedEvents.ShouldBeEmpty();
        It should_increment_the_commit_of_the_version = () => event_source.Version.Commit.ShouldEqual(2);
        It should_being_with_a_sequence_of_zero = () => event_source.Version.Sequence.ShouldEqual(0);
        It should_have_applied_the_event = () => event_source.EventApplied.ShouldBeTrue();
    }
}