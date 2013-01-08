#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
        /// Get a <see cref="CommittedEventStream"/> with events for specific given <see cref="EventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="EventSource"/> to get <see cref="IEvent">events</see> for</param>
        /// <param name="eventSourceId"><see cref="Guid">Id</see> of the specific <see cref="EventSource"/></param>
		/// <returns>All events for the aggregated root in an Event Stream</returns>
        CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId);

		/// <summary>
		/// Save events for a specific aggregated root
		/// </summary>
		/// <param name="eventsToSave"><see cref="UncommittedEventStream"></see><see cref="IEvent"/> to save as an Event Stream</param>
        /// <returns>The <see cref="CommittedEventStream"/> with all the events that was committed with their updated Ids</returns>
		CommittedEventStream Commit(UncommittedEventStream eventsToSave);

        /// <summary>
        /// Returns the last committed <see cref="EventSourceVersion">Event Source Version</see> for the <see cref="EventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="EventSource"/> to get <see cref="EventSourceVersion">version</see> for</param>
        /// <param name="eventSourceId"><see cref="Guid">Id</see> of the specific <see cref="EventSource"/></param>
        /// <returns>The last committed <see cref="EventSourceVersion">version</see></returns>
	    EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId);

        /// <summary>
        /// Get a batch of <see cref="IEvent">events</see> in the form of a 
        /// <see cref="CommittedEventStream">stream of events</see> 
        /// </summary>
        /// <param name="batchesToSkip">Number of batches to skip</param>
        /// <param name="batchSize">Size of each batch</param>
        /// <returns>A batch of <see cref="IEvent">events</see></returns>
        IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize);
	}
}