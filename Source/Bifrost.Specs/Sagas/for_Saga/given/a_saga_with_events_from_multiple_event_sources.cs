using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_with_events_from_multiple_event_sources : a_saga
    {
        protected static EventSourceId first_event_source_id = Guid.NewGuid();
        protected static EventSourceId second_event_source_id = Guid.NewGuid();
        protected static SimpleEvent first_event;
        protected static Mock<IEventEnvelope> first_event_envelope;
        protected static SimpleEvent second_event;
        protected static Mock<IEventEnvelope> second_event_envelope;

        Establish context = () =>
        {
            var firstEventStream = new UncommittedEventStream(first_event_source_id);
            first_event = new SimpleEvent(first_event_source_id);
            first_event_envelope = new Mock<IEventEnvelope>();
            first_event_envelope.SetupGet(e => e.EventSourceId).Returns(first_event_source_id);
            first_event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 0));
            firstEventStream.Append(first_event_envelope.Object, first_event);

            var secondEventStream = new UncommittedEventStream(second_event_source_id);
            second_event = new SimpleEvent(second_event_source_id);
            second_event_envelope = new Mock<IEventEnvelope>();
            second_event_envelope.SetupGet(e => e.EventSourceId).Returns(second_event_source_id);
            second_event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 1));
            secondEventStream.Append(second_event_envelope.Object, second_event);

            saga.Commit(firstEventStream);
            saga.Commit(secondEventStream);
        };
    }
}