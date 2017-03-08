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
    public interface IEventEnvelope
    {
        /// <summary>
        /// Gets the <see cref="EventId"/> representing the <see cref="IEvent"/>s
        /// </summary>
        EventId EventId { get; }

        /// <summary>
        /// Gets the <see cref="EvenMigrationLevel"/> for the <see cref="IEvent"/>
        /// </summary>
        EventGeneration Generation { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationResourceIdentifier">identifier</see> identifying the <see cref="IEvent"/>
        /// </summary>
        ApplicationResourceIdentifier Event { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceId">id</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceId EventSourceId { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationResourceIdentifier">identifier</see> identifying the <see cref="IEventSource"/>
        /// </summary>
        ApplicationResourceIdentifier EventSource { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceVersion">version</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceVersion Version { get; }

        /// <summary>
        /// Gets who or what the event was caused by.
        /// 
        /// Typically this would be the name of the user or system causing it
        /// </summary>
        CausedBy CausedBy { get; }

        /// <summary>
        /// Gets the time the event occurred
        /// </summary>
        DateTime Occurred { get; }

        /// <summary>
        /// Creates a new <see cref="EventEnvelope"/> with a different <see cref="EventId"/>
        /// </summary>
        /// <param name="eventId">The new <see cref="EventId"/></param>
        /// <returns>A copy of the <see cref="EventEnvelope"/> with a new Id </returns>
        EventEnvelope WithEventId(EventId eventId);
    }
}