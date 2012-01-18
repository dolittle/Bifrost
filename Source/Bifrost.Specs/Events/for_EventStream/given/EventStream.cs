using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventStream.given
{
    public class EventStream : IEnumerable<IEvent>
    {
        protected readonly List<IEvent> _events = new List<IEvent>();

        public EventStream(Guid eventSourceId)
        {
            EventSourceId = eventSourceId;
        }

        /// <summary>
        /// The Id of the Event Source (Aggregate Root) that this Event Stream relates to.
        /// </summary>
        public Guid EventSourceId { get; private set; }

        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return this.Count() > 0; } 
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return _events.Count; }
        }

        /// <summary>
        /// A generic enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<IEvent> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        /// <summary>
        /// An enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}