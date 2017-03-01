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
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> of the <see cref="IEventSource"/></param>
        public UncommittedEventStream(EventSourceId eventSourceId)
            : base(eventSourceId)
        {

        }

        /// <summary>
        /// Appends an event to the uncommitted event stream, setting the correct EventSourceId and Sequence Number for the event.
        /// </summary>
        /// <param name="envelope">The <see cref="EventEnvelope"/> representing the metadata for the event</param>
        /// <param name="event">The event to be appended.</param>
        public void Append(EventEnvelope envelope, IEvent @event)
        {
            ThrowIfEventIsNull(@event);
            @event.EventSourceId = EventSourceId;
            Events.Add(new EventEnvelopeAndEvent(envelope, @event));
        }

        void ThrowIfEventIsNull(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");
        }
    }
}