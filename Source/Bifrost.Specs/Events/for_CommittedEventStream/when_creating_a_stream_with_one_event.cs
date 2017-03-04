using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_CommittedEventStream
{
    public class when_creating_a_stream_with_one_event : given.an_empty_committed_event_stream
    {
        static IEvent @event;
        static Mock<IEventEnvelope> event_envelope;

        Establish context = () =>
        {
            @event = new SimpleEvent(event_source_id);
            event_envelope = new Mock<IEventEnvelope>();
        };

        Because of = () => event_stream = new CommittedEventStream(event_source_id, new [] { new EventAndEnvelope(event_envelope.Object, @event) });

        It should_have_events = () => event_stream.HasEvents.ShouldBeTrue();
        It should_have_an_event_count_of_1 = () => event_stream.Count.ShouldEqual(1);
    }
}