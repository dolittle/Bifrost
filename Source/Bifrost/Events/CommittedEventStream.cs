/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a special version of an eventstream
    /// that holds committed <see cref="IEvent">events</see>
    /// </summary>
    public class CommittedEventStream : IEnumerable<EventAndEnvelope>
    {
        List<EventAndEnvelope> _events = new List<EventAndEnvelope>();

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStream">CommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        public CommittedEventStream(EventSourceId eventSourceId)
        {
            EventSourceId = eventSourceId;
        }


        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStream">CommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        /// <param name="eventsWithEnvelope">The <see cref="IEvent">events</see> with their <see cref="EventEnvelope">envelopes</see></param>
        public CommittedEventStream(EventSourceId eventSourceId, IEnumerable<EventAndEnvelope> eventsWithEnvelope)
        {
            EventSourceId = eventSourceId;
            foreach (var eventAndEnvelope in eventsWithEnvelope)
            {
                EnsureEventIsValid(eventAndEnvelope);
                _events.Add(eventAndEnvelope);
            }
        }


        /// <summary>
        /// Gets the Id of the <see cref="IEventSource"/> that this <see cref="CommittedEventStream"/> relates to.
        /// </summary>
        public EventSourceId EventSourceId { get; private set; }


        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return Count > 0; }
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return _events.Count; }
        }

        /// <summary>
        /// Get a generic enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<EventAndEnvelope> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void EnsureEventIsValid(EventAndEnvelope eventAndEnvelope)
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