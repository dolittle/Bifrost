using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_and_an_event_source_and_multiple_committed_events : a_saga
    {
        protected static EventSourceId event_source_id = Guid.NewGuid();
        protected static SimpleEvent first_event;
        protected static Mock<IEventEnvelope> first_event_envelope;
        protected static SimpleEvent second_event;
        protected static Mock<IEventEnvelope> second_event_envelope;
        protected static Mock<IEventSource> event_source;

        Establish context = () =>
        {
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);

            var eventStream = new UncommittedEventStream(event_source_id);
            first_event = new SimpleEvent(event_source_id);
            first_event_envelope = new Mock<IEventEnvelope>();
            first_event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            first_event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 0));
            eventStream.Append(first_event_envelope.Object, first_event);

            second_event = new SimpleEvent(event_source_id);
            second_event_envelope = new Mock<IEventEnvelope>();
            second_event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            second_event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 1));
            eventStream.Append(second_event_envelope.Object, second_event);

            saga.Commit(eventStream);
        };
    }
}