using System;
using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_and_an_event_source : a_saga
    {
        protected static EventSourceId event_source_id = Guid.NewGuid();
        protected static Mock<IEventSource> event_source;

        Establish context = () =>
        {
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);
        };
    }
}