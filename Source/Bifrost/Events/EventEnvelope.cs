/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines the envelope for the event with all the metadata related to the event
    /// </summary>
    public class EventEnvelope
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventEnvelope"/>
        /// </summary>
        /// <param name="eventId"><see cref="EventId"/> for the <see cref="IEvent"/></param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> for the <see cref="IEventSource"/></param>
        /// <param name="eventSource"><see cref="ApplicationResourceIdentifier"/> representing the <see cref="IEventSource"/></param>
        /// <param name="version"><see cref="EventSourceVersion">Version</see> of the event related to the <see cref="IEventSource"/></param>
        /// <param name="causedBy"><see cref="string"/> representing which person or what system caused the event</param>
        /// <param name="occurred"><see cref="DateTime">When</see> the event occured</param>
        public EventEnvelope(EventId eventId, EventSourceId eventSourceId, ApplicationResourceIdentifier eventSource, EventSourceVersion version, string causedBy, DateTime occurred)
        {
            EventId = eventId;
            EventSourceId = eventSourceId;
            EventSource = eventSource;
            Version = version;
            CausedBy = causedBy;
            Occurred = occurred;
        }

        /// <summary>
        /// Gets the <see cref="EventId"/> representing the <see cref="IEvent"/>s
        /// </summary>
        public EventId EventId { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceId">id</see> of the <see cref="IEventSource"/>
        /// </summary>100
        public EventSourceId EventSourceId { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationResourceIdentifier">identifier</see> identifying the <see cref="EventSource"/>
        /// </summary>
        public ApplicationResourceIdentifier EventSource { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceVersion">version</see> of the <see cref="IEventSource"/>
        /// </summary>
        public EventSourceVersion Version { get; }

        /// <summary>
        /// Gets who or what the event was caused by.
        /// 
        /// Typically this would be the name of the user or system causing it
        /// </summary>
        public string CausedBy { get; }

        /// <summary>
        /// Gets the time the event occurred
        /// </summary>
        public DateTime Occurred { get; }

        /// <summary>
        /// Creates a new <see cref="EventEnvelope"/> with a different <see cref="EventId"/>
        /// </summary>
        /// <param name="eventId">The new <see cref="EventId"/></param>
        /// <returns>A copy of the <see cref="EventEnvelope"/> with a new Id </returns>
        public EventEnvelope WithEventId(EventId eventId)
        {
            return new EventEnvelope(eventId, EventSourceId, EventSource, Version, CausedBy, Occurred);
        }
    }
}
