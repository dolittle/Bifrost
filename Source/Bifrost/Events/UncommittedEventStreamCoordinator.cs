/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        IEventStore _eventStore;
        ICanSendCommittedEventStream _committedEventStreamSender;
        IEventEnvelopes _eventEnvelopes;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use for saving the events</param>
        /// <param name="committedEventStreamSender"><see cref="ICommittedEventStreamSender"/> send the <see cref="CommittedEventStream"/></param>
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for working with <see cref="EventEnvelope"/></param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            ICanSendCommittedEventStream committedEventStreamSender,
            IEventEnvelopes eventEnvelopes)
        {
            _eventStore = eventStore;
            _committedEventStreamSender = committedEventStreamSender;
            _eventEnvelopes = eventEnvelopes;
        }

        /// <inheritdoc/>
        public void Commit(TransactionCorrelationId correlationId, UncommittedEventStream uncommittedEventStream)
        {
            var envelopes = _eventEnvelopes.CreateFrom(uncommittedEventStream.EventSource, uncommittedEventStream.EventsAndVersion);

            var eventsAndEnvelopes = envelopes.Select(e => new EventAndEnvelope(
                    e.WithTransactionCorrelationId(correlationId)
                     .WithSequenceNumber(0)
                     .WithSequenceNumberForEventType(0),
                    uncommittedEventStream.EventsAndVersion.Where(ev=>ev.Version.Equals(e.Version)).First().Event
                ));

            _eventStore.Commit(eventsAndEnvelopes);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, eventsAndEnvelopes);
            _committedEventStreamSender.Send(committedEventStream);
        }
    }
}
