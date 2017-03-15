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
        protected static SimpleEvent second_event;
        protected static Mock<IEventSource> event_source;

        Establish context = () =>
        {
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);

            var first_event_Version = new EventSourceVersion(1, 0);
            var second_event_version = new EventSourceVersion(1, 1);

            var eventStream = new UncommittedEventStream(event_source.Object);
            first_event = new SimpleEvent(event_source_id);
            eventStream.Append(first_event, first_event_Version);

            second_event = new SimpleEvent(event_source_id);
            eventStream.Append(second_event, second_event_version);

            saga.Commit(eventStream);
        };
    }
}