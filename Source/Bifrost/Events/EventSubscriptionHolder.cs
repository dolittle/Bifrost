using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a holder of an <see cref="EventSubscription"/>
    /// </summary>
    public class EventSubscriptionHolder
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="EventSubscription"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the owner type, typically defined as the <see cref="EventSource"/>
        /// </summary>
        /// <remarks>
        /// Fully qualified assembly name of type
        /// </remarks>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the name of the method that is subscribing to the event
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the type of the event
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the logical name of the event
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the last event id the subscriber has processed
        /// </summary>
        public long LastEventId { get; set; }
    }
}
