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
        protected static EventAndVersion event_and_Version;

        Establish context = () =>
                                {
                                    var event_source_version = new EventSourceVersion(1, 1);
                                    var event_source_id = Guid.NewGuid();
                                    simple_event = new SimpleEvent(event_source_id);
                                    event_envelope = new Mock<IEventEnvelope>();
                                    event_envelope.SetupGet(e => e.EventSourceId).Returns(event_source_id);
                                    event_envelope.SetupGet(e => e.Version).Returns(event_source_version);

                                    event_and_Version = new EventAndVersion(simple_event, event_source_version);
                                    saga.SetUncommittedEvents(new[] { event_and_Version });
                                };

    }
}