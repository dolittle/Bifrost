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
    /// Represents a stream of events that are uncommitted
    /// </summary>
    public class UncommittedEventStream : IEnumerable<IEvent>
    {
        List<IEvent> _events = new List<IEvent>();
        List<EventAndVersion> _eventsAndVersion = new List<EventAndVersion>();


        /// <summary>
        /// Initializes a new instance of <see cref="UncommittedEventStream">UncommittedEventStream</see>
        /// </summary>
        /// <param name="eventSource">The <see cref="IEventSource"/> </param>
        public UncommittedEventStream(IEventSource eventSource)
        {
            EventSource = eventSource;
        }

        /// <summary>
        /// Gets the <see cref="IEventSource"/> for the <see cref="UncommittedEventStream"/>
        /// </summary>
        public IEventSource EventSource { get;  }

        /// <summary>
        /// Gets the Id of the <see cref="IEventSource"/> that this <see cref="UncommittedEventStream"/> relates to.
        /// </summary>
        public EventSourceId EventSourceId => EventSource.EventSourceId;

        /// <summary>
        /// Gets the <see cref="IEvent">events</see> and associated <see cref="EventSourceVersion">version</see>
        /// </summary>
        public IEnumerable<EventAndVersion> EventsAndVersion => _eventsAndVersion;

        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return this.Count > 0; }
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return _events.Count; }
        }

        /// <summary>
        /// Appends an event to the uncommitted event stream, setting the correct EventSourceId and Sequence Number for the event.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/>to be append</param>
        /// <param name="version">The <see cref="EventSourceVersion">version</see> of the <see cref="IEventSource"/> the <see cref="IEvent"/> is for</param>
        public void Append(IEvent @event, EventSourceVersion version)
        {
            ThrowIfEventIsNull(@event);
            ThrowIfEventBelongsToOtherEventSource(@event);
            @event.EventSourceId = EventSourceId;
            _events.Add(@event);
            _eventsAndVersion.Add(new EventAndVersion(@event, version));
        }

        /// <inerhitdoc/>
        public IEnumerator<IEvent> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        /// <inerhitdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        void ThrowIfEventBelongsToOtherEventSource(IEvent @event)
        {
            if (@event.EventSourceId != EventSourceId)
                throw new EventBelongsToOtherEventSource(@event.EventSourceId, EventSourceId);
        }

        void ThrowIfEventIsNull(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");
        }
    }
}