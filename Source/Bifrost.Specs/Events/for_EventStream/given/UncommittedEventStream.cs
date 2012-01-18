using System;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventStream.given
{
    public class UncommittedEventStream : EventStream
    {
        public static readonly Guid EventIsUnattached = Guid.Empty;
        public static readonly int EventIsUnsequenced = -1;

        public UncommittedEventStream(Guid eventSourceId) : base(eventSourceId)
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

            _events.Add(@event);
        }

        private void AttachAndSequenceEvent(IEvent @event)
        {
            @event.EventSourceId = EventSourceId;
        }

        private void EnsureEventCanBeAppendedToThisEventSource(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");

            if (@event.EventSourceId != EventSourceId)
                throw new ArgumentException(
                     string.Format("Cannot append an event that has already been appended to another event source.  Already appended to event source id {0}}.",
                                  @event.EventSourceId));
        }
    }
}