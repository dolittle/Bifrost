/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a stream of events that are uncommitted
    /// </summary>
    public class UncommittedEventStream : EventStream
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UncommittedEventStream">UncommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">Id of the event source - typically an <see cref="AggregateRoot">AggregatedRoot</see></param>
        public UncommittedEventStream(Guid eventSourceId)
            : base(eventSourceId)
        {

        }

        /// <summary>
        /// Appends an event to the uncommitted event stream, setting the correct EventSourceId and Sequence Number for the event.
        /// </summary>
        /// <param name="event">The event to be appended.</param>
        public void Append(IEvent @event)
        {
            EnsureEventCanBeAppendedToThisEventSource(@event);
            AttachAndSequenceEvent(@event);

            Events.Add(@event);
        }

        private void AttachAndSequenceEvent(IEvent @event)
        {
            @event.EventSourceId = EventSourceId;
        }

        private void EnsureEventCanBeAppendedToThisEventSource(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");
        }
    }
}