/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
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
            var events = new List<EventAndEnvelope>();
            foreach (var eventAndVersion in uncommittedEventStream.EventsAndVersion)
            {
                var envelope = _eventEnvelopes.CreateFrom(uncommittedEventStream.EventSource, eventAndVersion.Event);
                events.Add(new EventAndEnvelope(envelope, eventAndVersion.Event));
            }
            _eventStore.Commit(events);

            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, events);
            _committedEventStreamSender.Send(committedEventStream);
        }
    }
}
