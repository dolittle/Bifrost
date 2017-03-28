/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    [Singleton]
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        IEventStore _eventStore;
        IEventSourceVersions _eventSourceVersions;
        ICanSendCommittedEventStream _committedEventStreamSender;
        IEventEnvelopes _eventEnvelopes;
        IEventSequenceNumbers _eventSequenceNumbers;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use for saving the events</param>
        /// <param name="eventSourceVersions"><see cref="IEventSourceVersions"/> for working with the version for the <see cref="IEventSource"/></param>
        /// <param name="committedEventStreamSender"><see cref="ICanSendCommittedEventStream"/> send the <see cref="CommittedEventStream"/></param>
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for working with <see cref="EventEnvelope"/></param>
        /// <param name="eventSequenceNumbers"><see cref="IEventSequenceNumbers"/> for allocating <see cref="EventSequenceNumber">sequence number</see> for <see cref="IEvent">events</see></param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            IEventSourceVersions eventSourceVersions,
            ICanSendCommittedEventStream committedEventStreamSender,
            IEventEnvelopes eventEnvelopes,
            IEventSequenceNumbers eventSequenceNumbers)
        {
            _eventStore = eventStore;
            _eventSourceVersions = eventSourceVersions;
            _committedEventStreamSender = committedEventStreamSender;
            _eventEnvelopes = eventEnvelopes;
            _eventSequenceNumbers = eventSequenceNumbers;
        }

        /// <inheritdoc/>
        public void Commit(TransactionCorrelationId correlationId, UncommittedEventStream uncommittedEventStream)
        {
            var envelopes = _eventEnvelopes.CreateFrom(uncommittedEventStream.EventSource, uncommittedEventStream.EventsAndVersion);
            var envelopesAsArray = envelopes.ToArray();
            var eventsAsArray = uncommittedEventStream.ToArray();

            var eventsAndEnvelopes = new List<EventAndEnvelope>();
            for( var eventIndex=0; eventIndex<eventsAsArray.Length; eventIndex++ )
            {
                var envelope = envelopesAsArray[eventIndex];
                var @event = eventsAsArray[eventIndex];
                eventsAndEnvelopes.Add(new EventAndEnvelope(
                    envelope
                        .WithTransactionCorrelationId(correlationId)
                        .WithSequenceNumber(_eventSequenceNumbers.Next())
                        .WithSequenceNumberForEventType(_eventSequenceNumbers.NextForType(envelope.Event)),
                    @event
                ));
            }

            _eventStore.Commit(eventsAndEnvelopes);
            _eventSourceVersions.SetFor(envelopesAsArray[0].EventSource, envelopesAsArray[0].EventSourceId, envelopesAsArray[envelopesAsArray.Length - 1].Version);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, eventsAndEnvelopes);
            _committedEventStreamSender.Send(committedEventStream);
        }
    }
}
