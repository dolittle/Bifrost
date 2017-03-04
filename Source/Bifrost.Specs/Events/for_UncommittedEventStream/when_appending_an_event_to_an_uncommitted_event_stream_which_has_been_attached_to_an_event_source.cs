using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_UncommittedEventStream
{
    public class when_appending_an_event_to_an_uncommitted_event_stream_which_has_been_attached_to_an_event_source
        : given.an_empty_uncommitted_event_stream
    {
        static Exception Exception;
        static IEvent @event;
        static Mock<IEventEnvelope> event_envelope;

        Establish context = () =>
        {
            @event = new SimpleEvent(event_source_id);
            event_envelope = new Mock<IEventEnvelope>();
        };

        Because of = () => Exception = Catch.Exception(() => event_stream.Append(event_envelope.Object, @event));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();
    }
}