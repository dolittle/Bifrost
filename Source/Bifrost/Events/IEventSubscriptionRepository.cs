#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
    /// Defines a repository for working with <see cref="EventSubscription">EventSubscriptions</see>
    /// </summary>
    public interface IEventSubscriptionRepository
    {
        /// <summary>
        /// Get all subscriptions for a specific <see cref="IEvent"/> type
        /// </summary>
        /// <param name="eventType">Type of <see cref="IEvent"/> to get for</param>
        /// <returns>All <see cref="EventSubscription">EventSubscriptions</see> for the given type</returns>
        IEnumerable<EventSubscription> GetForEvent(Type eventType);

        /// <summary>
        /// Get a specific subscription
        /// </summary>
        /// <param name="id">Identifier of the <see cref="EventSubscription"/></param>
        /// <returns>The specific <see cref="EventSubscription">EventSubscriptions</see></returns>
        EventSubscription Get(Guid id);

        /// <summary>
        /// Reset last event id for a specific subscription
        /// </summary>
        /// <param name="id">Identifier of the <see cref="EventSubscription"/> to reset</param>
        void ResetLastEventId(Guid id);

        /// <summary>
        /// Reset last event id for all subscriptions
        /// </summary>
        void ResetLastEventForAllSubscriptions();

        /// <summary>
        /// Get all subscriptions available
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see> </returns>
        IEnumerable<EventSubscription> GetAll();

        /// <summary>
        /// Add a <see cref="EventSubscription"/>
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to add</param>
        void Add(EventSubscription subscription);

        /// <summary>
        /// Update an existing <see cref="EventSubscription"/>
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to update</param>
        void Update(EventSubscription subscription);
    }
}
