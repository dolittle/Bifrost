using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Lifecycle;
using System.Collections.Generic;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(UncommittedEventStream))]
    public class when_committing : given.an_uncommitted_event_stream_coordinator
    {
        static Mock<IEventSource> event_source;
        static EventSourceId event_source_id = Guid.NewGuid();
        static IEnumerable<EventAndEnvelope>   uncommitted_events;
        static UncommittedEventStream uncommitted_event_stream;
        static TransactionCorrelationId transaction_correlation_id;
        static EventSourceVersion event_source_version;

        Establish context = () =>
        {
            event_source_version = new EventSourceVersion(4, 2);
            
            var @event = new Mock<IEvent>();
            var eventEnvelope = new Mock<IEventEnvelope>();
            var eventAndEnvelope = new EventAndEnvelope(eventEnvelope.Object, @event.Object);
            var eventAndVersion = new EventAndVersion(@event.Object, event_source_version);

            transaction_correlation_id = Guid.NewGuid();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            uncommitted_events = new EventAndEnvelope[] { eventAndEnvelope };
            uncommitted_event_stream = new UncommittedEventStream(event_source.Object);
            uncommitted_event_stream.Append(@event.Object, event_source_version);


            event_store_mock.Setup(e => e.Commit(uncommitted_events));
        };

        Because of = () => coordinator.Commit(transaction_correlation_id, uncommitted_event_stream);

        It should_insert_event_stream_into_repository = () => event_store_mock.Verify(e => e.Commit(uncommitted_events), Times.Once());
        //It should_send_the_committed_event_stream = () => committed_event_stream_sender_mock.Verify(c => c.Send(committed_event_stream), Times.Once());
    }
}
