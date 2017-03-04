/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a special version of an <see cref="EventStream">EventStream</see>
    /// that holds committed <see cref="IEvent">events</see>
    /// </summary>
    public class CommittedEventStream : EventStream
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStream">CommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        public CommittedEventStream(EventSourceId eventSourceId)
            : base(eventSourceId)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStream">CommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        /// <param name="eventsWithEnvelope">The <see cref="IEvent">events</see> with their <see cref="EventEnvelope">envelopes</see></param>
        public CommittedEventStream(EventSourceId eventSourceId, IEnumerable<EventWithEnvelope> eventsWithEnvelope)
            : base(eventSourceId)
        {
            foreach (var eventAndEnvelope in eventsWithEnvelope)
            {
                EnsureEventIsValid(eventAndEnvelope);
                Events.Add(eventAndEnvelope);
            }
        }

        void EnsureEventIsValid(EventWithEnvelope eventAndEnvelope)
        {
            if (eventAndEnvelope.Event == null)
                throw new ArgumentNullException("Cannot append a null event");

            if (eventAndEnvelope.Event.EventSourceId != EventSourceId)
                throw new ArgumentException(
                    string.Format("Cannot append an event from a different source.  Expected source {0} but got {1}.",
                                  EventSourceId, eventAndEnvelope.Event.EventSourceId)
                    );
        }
    }
}