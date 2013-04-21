using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventStream.given
{
    public class CommittedEventStream : EventStream
    {
        public CommittedEventStream(Guid eventSourceId) : base(eventSourceId)
        {
        }

        public void Append(IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                Append(@event);
            }
        }

        private void Append(IEvent @event)
        {
            EnsureEventIsValid(@event);

            _events.Add(@event);
        }

        /// <summary>
        /// Validates that the Id and Sequence number are as expected.
        /// </summary>
        /// <param name="event">Event to validate</param>
        private void EnsureEventIsValid(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");

            var lastEvent = this.LastOrDefault();
            if (lastEvent != null)
                if (@event.Version.Sequence != this.Last().Version.Sequence + 1)
                    throw new EventOutOfSequenceException();

            if (@event.EventSourceId != EventSourceId)
                throw new ArgumentException(
                    string.Format("Cannot append an event from a different source.  Expected source {0} but got {1}.",
                                  EventSourceId, @event.EventSourceId)
                    );
        }
    }
}