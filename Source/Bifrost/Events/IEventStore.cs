/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
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
        /// <param name="eventSourceId"><see cref="EventSourceId">Id</see> of the specific <see cref="EventSource"/></param>
		/// <returns>All events for the aggregated root in an Event Stream</returns>
        CommittedEventStream GetForEventSource(IEventSource eventSource, EventSourceId eventSourceId);

		/// <summary>
		/// Save events for a specific aggregated root
		/// </summary>
        /// <param name="uncommittedEventStream"><see cref="UncommittedEventStream"></see> to save</param>
        /// <returns>The <see cref="CommittedEventStream"/> with all the events that was committed with their updated Ids</returns>
        CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream);

        /// <summary>
        /// Returns the last committed <see cref="EventSourceVersion">Event Source Version</see> for the <see cref="IEventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> to get <see cref="EventSourceVersion">version</see> for</param>
        /// <param name="eventSourceId"><see cref="EventSourceId">Id</see> of the specific <see cref="EventSource"/></param>
        /// <returns>The last committed <see cref="EventSourceVersion">version</see></returns>
	    EventSourceVersion GetLastCommittedVersion(IEventSource eventSource, EventSourceId eventSourceId);
	}
}