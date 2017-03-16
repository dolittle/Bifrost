/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

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
        /// <param name="eventSource"><see cref="IEventSource"/> to get <see cref="IEvent">events</see> for</param>
		/// <returns>All events for the aggregated root in an Event Stream</returns>
        CommittedEventStream GetFor(IEventSource eventSource);

		/// <summary>
		/// Save events for a specific aggregated root
		/// </summary>
        /// <param name="eventsAndEnvelopes"><see cref="IEnumerable{T}">Events and envelopes</see> to commit</param>
        void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes);

        /// <summary>
        /// Returns the last committed <see cref="EventSourceVersion">Event Source Version</see> for the <see cref="IEventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to get <see cref="EventSourceVersion">version</see> for</param>
        /// <returns>The last committed <see cref="EventSourceVersion">version</see></returns>
	    EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource);
	}
}