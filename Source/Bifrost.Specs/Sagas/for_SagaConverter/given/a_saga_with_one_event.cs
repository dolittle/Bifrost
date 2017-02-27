using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
    public class a_saga_with_one_event : a_saga_converter_and_a_saga
    {
        protected static SimpleEvent simple_event;

        Establish context = () =>
                                {
                                    simple_event = new SimpleEvent(Guid.NewGuid());
                                    saga.SetUncommittedEvents(new[] { simple_event });
                                };

    }
}