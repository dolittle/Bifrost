using Machine.Specifications;
using Bifrost.Events;
using System;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Lifecycle;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(UncommittedEventStreamCoordinator))]
    public class when_committing : given.an_uncommitted_event_stream_coordinator
    {
        static Mock<IEventSource> event_source;
        static EventSourceId event_source_id = Guid.NewGuid();
        static IEnumerable<EventAndEnvelope>   uncommitted_events;
        static UncommittedEventStream uncommitted_event_stream;
        static TransactionCorrelationId transaction_correlation_id;
        static EventSourceVersion event_source_version;
        static Mock<IEvent> @event;
        static Mock<IEventEnvelope> event_envelope;

        Establish context = () =>
        {
            event_source_version = new EventSourceVersion(4, 2);
            
            @event = new Mock<IEvent>();
            @event.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            event_envelope = new Mock<IEventEnvelope>();
            var eventAndVersion = new EventAndVersion(@event.Object, event_source_version);

            transaction_correlation_id = Guid.NewGuid();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            uncommitted_event_stream = new UncommittedEventStream(event_source.Object);
            uncommitted_event_stream.Append(@event.Object, event_source_version);

            //event_envelopes.Setup(e => e.CreateFrom(event_source.Object, @event.Object)).Returns(event_envelope.Object);

            event_store_mock.Setup(e => e.Commit(uncommitted_events));

            event_store_mock.Setup(e => e.Commit(Moq.It.IsAny<IEnumerable<EventAndEnvelope>>())).Callback(
                (IEnumerable<EventAndEnvelope> e) => uncommitted_events = e);
        };

        Because of = () => coordinator.Commit(transaction_correlation_id, uncommitted_event_stream);

        It should_commit_only_one_event_and_envelope = () => uncommitted_events.Count().ShouldEqual(1);
        It should_commit_event_with_correct_envelope_to_event_store = () => uncommitted_events.First().Envelope.ShouldEqual(event_envelope.Object);
        It should_commit_insert_event_with_correct_event_to_event_store = () => uncommitted_events.First().Event.ShouldEqual(@event.Object);
        //It should_send_the_committed_event_stream = () => committed_event_stream_sender_mock.Verify(c => c.Send(committed_event_stream), Times.Once());
    }
}
