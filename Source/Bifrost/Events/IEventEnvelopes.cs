/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
        /// <returns><see cref="IEventEnvelope"/></returns>
        IEventEnvelope CreateFrom(IEventSource eventSource, IEvent @event);
    }
}
