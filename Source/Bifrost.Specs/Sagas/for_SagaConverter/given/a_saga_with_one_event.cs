using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
    public class a_saga_with_one_event : a_saga_converter_and_a_saga
    {
        protected static SimpleEvent simple_event;
        protected static Mock<IEventEnvelope> event_envelope;
        protected static EventAndEnvelope event_and_envelope;

        Establish context = () =>
                                {
                                    var event_source_id = Guid.NewGuid();
                                    simple_event = new SimpleEvent(event_source_id);
                                    event_envelope = new Mock<IEventEnvelope>();
                                    event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
                                    event_envelope.SetupGet(e => e.Version).Returns(new EventSourceVersion(1, 1));

                                    event_and_envelope = new EventAndEnvelope(event_envelope.Object, simple_event);
                                    saga.SetUncommittedEvents(new[] { event_and_envelope });
                                };

    }
}