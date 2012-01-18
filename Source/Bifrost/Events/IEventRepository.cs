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
using System.Linq.Expressions;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a repository for working with <see cref="IEvent">Events</see>
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Get a specific event by its Id
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>An instance of the event</returns>
        IEvent GetById(Guid id);

		/// <summary>
		/// Get a specific event as Json
		/// </summary>
		/// <param name="id">Id of the event</param>
		/// <returns>A string containing Json</returns>
    	string GetByIdAsJson(Guid id);

        /// <summary>
        /// Get a set of events based upon a set of Ids
        /// </summary>
        /// <param name="ids">Ids of events to get</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IEvent"/> containing the events</returns>
        IEnumerable<IEvent> GetByIds(IEnumerable<Guid> ids);

        /// <summary>
        /// Get a set of events for a specific aggregated root
        /// </summary>
        /// <param name="aggregatedRootType">Type of the aggregated root</param>
        /// <param name="aggregateId">Id of the aggregated root</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IEvent"/> for the aggregated root</returns>
        IEnumerable<IEvent> GetForAggregatedRoot(Type aggregatedRootType, Guid aggregateId);

        /// <summary>
        /// Get events based on a set of subscriptions - any events of the given type in the subscriptions with
        /// that are unprocessed according to the subscription 
        /// </summary>
        /// <param name="subscriptions">Subscriptions to get for</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IEvent"/> for the subscriptions, if any</returns>
        IEnumerable<IEvent> GetUnprocessedEventsForSubscriptions(IEnumerable<EventSubscription> subscriptions);

        /// <summary>
        /// Insert a set of events
        /// </summary>
        /// <param name="events"><see cref="IEnumerable{T}"/> of <see cref="IEvent"/> to insert</param>
        void Insert(IEnumerable<IEvent> events);

        /// <summary>
        /// Gets the last committed <seealso cref="EventSourceVersion">Event Source Version</seealso> for this aggregate root
        /// </summary>
        /// <param name="aggregatedRootType">Type of the aggregate root</param>
        /// <param name="aggregateId">Id of the aggregate root</param>
        /// <returns>The <seealso cref="EventSourceVersion">Event Source Version</seealso> of the last committed event for this aggregate root</returns>
        EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId);
    }
}