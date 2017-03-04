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

        Establish context = () =>
                                {
                                    var event_source_id = Guid.NewGuid();
                                    simple_event = new SimpleEvent(event_source_id);
                                    var event_envelope = new Mock<IEventEnvelope>();
                                    saga.SetUncommittedEvents(new[] { new EventAndEnvelope(event_envelope.Object, simple_event) });
                                };

    }
}