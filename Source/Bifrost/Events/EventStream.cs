/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a collection of events in the order that they were applied.
    /// </summary>
    public class EventStream : IEnumerable<IEvent>
    {
		/// <summary>
		/// Gets a list of all the events in the stream
		/// </summary>
        protected readonly List<IEvent> Events = new List<IEvent>();

		/// <summary>
		/// Initializes a new <see cref="EventStream">EventStream</see>
		/// </summary>
		/// <param name="eventSourceId">Id of the event source - typically an <see cref="AggregateRoot">AggregatedRoot</see></param>
        public EventStream(Guid eventSourceId)
        {
            EventSourceId = eventSourceId;
        }

        /// <summary>
        /// Gets the Id of the Event Source (Aggregate Root) that this Event Stream relates to.
        /// </summary>
        public Guid EventSourceId { get; private set; }

        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return this.Count()  > 0; } 
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return Events.Count; }
        }

        /// <summary>
        /// Get a generic enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<IEvent> GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}