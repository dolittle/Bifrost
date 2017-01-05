/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="eventSourceId">Id of the event source - typically an <see cref="AggregateRoot">AggregatedRoot</see></param>
        public CommittedEventStream(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        /// <summary>
        /// Append a set of events to the stream.  Events will be applied in Sequence, not in the order they are passed in.
        /// </summary>
        /// <param name="events"><see cref="IEnumerable{T}">IEnumerable</see> of <see cref="IEvent">events</see> to append</param>
        public void Append(IEnumerable<IEvent> events)
        {
            var orderedEvents = events.OrderBy(e => e.Version);
            foreach(var @event in orderedEvents)
            {
                Append(@event);
            }
        }

        /// <summary>
        /// Append a single event to the stream.
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to append</param>
        public void Append(IEvent @event)
        {
            EnsureEventIsValid(@event);
            Events.Add(@event);
        }

        private void EnsureEventIsValid(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");

            if (@event.EventSourceId != EventSourceId)
                throw new ArgumentException(
                    string.Format("Cannot append an event from a different source.  Expected source {0} but got {1}.",
                                  EventSourceId, @event.EventSourceId)
                    );
        }
    }
}