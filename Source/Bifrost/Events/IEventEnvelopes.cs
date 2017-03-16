/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system for working with <see cref="EventEnvelope"/>
    /// </summary>
    public interface IEventEnvelopes
    {
        /// <summary>
        /// Create a <see cref="EventEnvelope"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to create <see cref="EventEnvelope"/> from</param>
        /// <param name="event"><see cref="IEvent"/> to create <see cref="EventEnvelope"/> from</param>
        /// <param name="version"><see cref="EventSourceVersion">Version</see> of the <see cref="IEvent"/> on an <see cref="IEventSource"/></param>
        /// <returns><see cref="IEventEnvelope"/></returns>
        IEventEnvelope CreateFrom(IEventSource eventSource, IEvent @event, EventSourceVersion version);

        /// <summary>
        /// Create an <see cref="IEnumerable{IEventEnvelope}"/> from <see cref="IEnumerable{EventAndVersion}"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to create from</param>
        /// <param name="eventsAndVersion"><see cref="IEnumerable{EventAndVersion}">Events and version</see> to create from</param>
        /// <returns><see cref="IEnumerable{IEventEnvelope}">Event envelopes</see></returns>
        IEnumerable<IEventEnvelope> CreateFrom(IEventSource eventSource, IEnumerable<EventAndVersion> eventsAndVersion);
    }
}
