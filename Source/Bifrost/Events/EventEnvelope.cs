/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventEnvelope"/>; the envelope for the event with all the metadata related to the event
    /// </summary>
    public class EventEnvelope : IEventEnvelope
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventEnvelope"/>
        /// </summary>
        /// <param name="eventId"><see cref="EventId"/> for the <see cref="IEvent"/></param>
        /// <param name="generation"><see cref="EventGeneration"/> for the <see cref="IEvent"/> </param>
        /// <param name="event"><see cref="IApplicationResourceIdentifier"/> representing the <see cref="IEvent"/></param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> for the <see cref="IEventSource"/></param>
        /// <param name="eventSource"><see cref="IApplicationResourceIdentifier"/> representing the <see cref="IEventSource"/></param>
        /// <param name="version"><see cref="EventSourceVersion">Version</see> of the event related to the <see cref="IEventSource"/></param>
        /// <param name="causedBy"><see cref="string"/> representing which person or what system caused the event</param>
        /// <param name="occurred"><see cref="DateTime">When</see> the event occured</param>
        public EventEnvelope(
            EventId eventId,
            EventGeneration generation, 
            IApplicationResourceIdentifier @event, 
            EventSourceId eventSourceId, 
            IApplicationResourceIdentifier eventSource, 
            EventSourceVersion version, 
            CausedBy causedBy, 
            DateTime occurred)
        {
            EventId = eventId;
            Generation = generation;
            Event = @event;
            EventSourceId = eventSourceId;
            EventSource = eventSource;
            Version = version;
            CausedBy = causedBy;
            Occurred = occurred;
        }

        /// <inheritdoc/>
        public EventId EventId { get; }

        /// <inheritdoc/>
        public EventGeneration Generation { get; }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier Event { get; }

        /// <inheritdoc/>
        public EventSourceId EventSourceId { get; }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier EventSource { get; }

        /// <inheritdoc/>
        public EventSourceVersion Version { get; }

        /// <inheritdoc/>
        public CausedBy CausedBy { get; }

        /// <inheritdoc/>
        public DateTime Occurred { get; }

        /// <inheritdoc/>
        public EventEnvelope WithEventId(EventId eventId)
        {
            return new EventEnvelope(eventId, Generation, Event, EventSourceId, EventSource, Version, CausedBy, Occurred);
        }
    }
}
