using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Applications;
using Bifrost.Events;
using Bifrost.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator
{
    [Subject(typeof(UncommittedEventStreamCoordinator))]
    public class when_committing_a_single_uncommitted_event : given.an_uncommitted_event_stream_coordinator
    {
        static EventSequenceNumber sequence_number = 42L;
        static EventSequenceNumber sequence_number_for_type = 43L;

        static string sequence_string = string.Empty;

        static TransactionCorrelationId transaction_correlation_id;

        static Mock<IEventSource> event_source;
        static EventSourceId event_source_id = Guid.NewGuid();
        static EventSourceVersion event_source_version;
        static Mock<IApplicationResourceIdentifier> event_source_identifier;

        static IEnumerable<EventAndEnvelope> uncommitted_events;
        static UncommittedEventStream uncommitted_event_stream;

        static CommittedEventStream committed_event_stream;

        
        static Mock<IEvent> @event;
        static IEventEnvelope event_envelope;
        static Mock<IApplicationResourceIdentifier> event_identifier;
        

        Establish context = () =>
        {
            transaction_correlation_id = Guid.NewGuid();

            event_source_version = new EventSourceVersion(4, 2);
            event_source_identifier = new Mock<IApplicationResourceIdentifier>();
            event_source = new Mock<IEventSource>();
            event_source.SetupGet(e => e.EventSourceId).Returns(event_source_id);

            event_identifier = new Mock<IApplicationResourceIdentifier>();
            @event = new Mock<IEvent>();
            @event.SetupGet(e => e.EventSourceId).Returns(event_source_id);
            event_envelope = new EventEnvelope(
                TransactionCorrelationId.NotSet,
                EventId.New(),
                EventSequenceNumber.Zero,
                EventSequenceNumber.Zero,
                EventGeneration.First,
                event_identifier.Object,
                event_source_id,
                event_source_identifier.Object,
                event_source_version,
                CausedBy.Unknown,
                DateTimeOffset.UtcNow
            );

            var event_and_envelope = new EventAndEnvelope(event_envelope, @event.Object);

            var eventAndVersion = new EventAndVersion(@event.Object, event_source_version);

            uncommitted_event_stream = new UncommittedEventStream(event_source.Object);
            uncommitted_event_stream.Append(@event.Object, event_source_version);

            event_envelopes.Setup(e => e.CreateFrom(event_source.Object, uncommitted_event_stream.EventsAndVersion)).Returns(new IEventEnvelope[]
            {
                event_envelope
            });

            event_store.Setup(e => e.Commit(uncommitted_events));

            event_store.Setup(e => e.Commit(Moq.It.IsAny<IEnumerable<EventAndEnvelope>>())).Callback(
                (IEnumerable<EventAndEnvelope> e) =>
                {
                    uncommitted_events = e;
                    sequence_string = sequence_string + "1";
                });

            event_sequence_numbers.Setup(e => e.Next()).Returns(sequence_number);
            event_sequence_numbers.Setup(e => e.NextForType(event_identifier.Object)).Returns(sequence_number_for_type);

            committed_event_stream_sender.Setup(e => e.Send(Moq.It.IsAny<CommittedEventStream>()))
                .Callback((CommittedEventStream c) =>
                {
                    committed_event_stream = c;
                    sequence_string = sequence_string + "2";
                });


        };

        Because of = () => coordinator.Commit(transaction_correlation_id, uncommitted_event_stream);

        It should_commit_insert_event_with_correct_event_to_event_store = () => uncommitted_events.First().Event.ShouldEqual(@event.Object);

        It should_commit_one_event = () => uncommitted_events.Count().ShouldEqual(1);
        It should_hold_the_correct_sequence_number_when_committing = () => uncommitted_events.First().Envelope.SequenceNumber.ShouldEqual(sequence_number);
        It should_hold_the_correct_sequence_number_for_event_type_when_committing = () => uncommitted_events.First().Envelope.SequenceNumberForEventType.ShouldEqual(sequence_number_for_type);
        It should_hold_the_correct_correlation_id_when_committing = () => uncommitted_events.First().Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_event_when_committing = () => uncommitted_events.First().Event.ShouldEqual(@event.Object);

        It should_send_one_event = () => committed_event_stream.Count.ShouldEqual(1);
        It should_hold_the_correct_sequence_number_when_sending = () => committed_event_stream.First().Envelope.SequenceNumber.ShouldEqual(sequence_number);
        It should_hold_the_correct_sequence_number_for_event_type_when_sending = () => committed_event_stream.First().Envelope.SequenceNumberForEventType.ShouldEqual(sequence_number_for_type);
        It should_hold_the_correct_correlation_id_when_sending = () => committed_event_stream.First().Envelope.CorrelationId.ShouldEqual(transaction_correlation_id);
        It should_hold_the_correct_event_when_sending = () => committed_event_stream.First().Event.ShouldEqual(@event.Object);

        It should_commit_before_sending = () => sequence_string.ShouldEqual("12");
    }
}
