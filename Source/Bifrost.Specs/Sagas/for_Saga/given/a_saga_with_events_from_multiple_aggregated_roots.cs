using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_with_events_from_multiple_aggregated_roots : a_saga
    {
        protected static EventSourceId first_aggregated_root_id = Guid.NewGuid();
        protected static EventSourceId second_aggregated_root_id = Guid.NewGuid();
        protected static SimpleEvent first_event;
        protected static Mock<IEventEnvelope> first_event_envelope;
        protected static SimpleEvent second_event;
        protected static Mock<IEventEnvelope> second_event_envelope;

        Establish context = () =>
        {
            var firstEventStream = new UncommittedEventStream(first_aggregated_root_id);
            first_event = new SimpleEvent(first_aggregated_root_id);
            first_event_envelope = new Mock<IEventEnvelope>();
            firstEventStream.Append(first_event_envelope.Object, first_event);

            var secondEventStream = new UncommittedEventStream(second_aggregated_root_id);
            second_event = new SimpleEvent(second_aggregated_root_id);
            second_event_envelope = new Mock<IEventEnvelope>();
            secondEventStream.Append(second_event_envelope.Object, second_event);

            saga.Commit(firstEventStream);
            saga.Commit(secondEventStream);
        };
    }
}