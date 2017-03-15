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
        protected static Mock<IEventSource> first_event_source;
        protected static Mock<IEventSource> second_event_source;
        protected static SimpleEvent first_event;
        protected static SimpleEvent second_event;

        Establish context = () =>
        {
            first_event_source = new Mock<IEventSource>();
            second_event_source = new Mock<IEventSource>();
            first_event_source.SetupGet(e => e.EventSourceId).Returns(first_event_source_id);
            second_event_source.SetupGet(e => e.EventSourceId).Returns(second_event_source_id);

            var first_event_Version = new EventSourceVersion(1, 0);
            var second_event_version = new EventSourceVersion(1, 1);

            var firstEventStream = new UncommittedEventStream(first_event_source.Object);
            first_event = new SimpleEvent(first_event_source_id);
            firstEventStream.Append(first_event, first_event_Version);

            var secondEventStream = new UncommittedEventStream(second_event_source.Object);
            second_event = new SimpleEvent(second_event_source_id);
            secondEventStream.Append(second_event, second_event_version);

            saga.Commit(firstEventStream);
            saga.Commit(secondEventStream);
        };
    }
}