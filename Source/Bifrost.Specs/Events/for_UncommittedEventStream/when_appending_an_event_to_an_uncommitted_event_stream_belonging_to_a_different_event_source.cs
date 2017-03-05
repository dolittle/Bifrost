using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_UncommittedEventStream
{
    public class when_appending_an_event_to_an_uncommitted_event_stream_belonging_to_a_different_event_source
        : given.an_empty_uncommitted_event_stream
    {
        static Exception Exception;
        static IEvent @event;
        static Mock<IEventEnvelope> event_envelope;

        Establish context = () =>
        {
            @event = new SimpleEvent(event_source_id);
            event_envelope = new Mock<IEventEnvelope>();
            event_envelope.SetupGet(e => e.EventSourceId).Returns(Guid.NewGuid());
        };

        Because of = () => Exception = Catch.Exception(() => event_stream.Append(event_envelope.Object, @event));

        It should_throw_event_belongs_to_other_eventSource = () => Exception.ShouldBeOfExactType<EventBelongsToOtherEventSource>();
    }
}