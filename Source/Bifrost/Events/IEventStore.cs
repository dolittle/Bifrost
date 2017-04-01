/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a repository that holds events generated
    /// </summary>
    public interface IEventStore
	{
        /// <summary>
        /// Get a <see cref="CommittedEventStream"/> with events for specific given <see cref="IEventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IApplicationResourceIdentifier">Identifier</see> representing the <see cref="IEventSource"/> to get <see cref="IEvent">events</see> for</param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> identifying the <see cref="IEventSource"/></param>
		/// <returns>All events for the aggregated root in an Event Stream</returns>
        IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId);

        /// <summary>
        /// Check if there are <see cref="IEvent">events</see> for <see cref="IEventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IApplicationResourceIdentifier">Identifier</see> representing the <see cref="IEventSource"/> to get <see cref="IEvent">events</see> for</param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> identifying the <see cref="IEventSource"/></param>
        /// <returns>True if there are events, false if not</returns>
        bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId);

        /// <summary>
        /// Get the latest version of an <see cref="IEventSource"/> based on the <see cref="IEvent">events</see> stored
        /// </summary>
        /// <param name="eventSource"><see cref="IApplicationResourceIdentifier">Identifier</see> representing the <see cref="IEventSource"/> to get <see cref="IEvent">events</see> for</param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> identifying the <see cref="IEventSource"/></param>
        /// <returns>The actual <see cref="EventSourceVersion"/> of the latest <see cref="IEvent"/> for the <see cref="IEventSource"/></returns>
        EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId);

        /// <summary>
        /// Save events for a specific aggregated root
        /// </summary>
        /// <param name="eventsAndEnvelopes"><see cref="IEnumerable{T}">Events and envelopes</see> to commit</param>
        void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes);
	}
}