#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a manager for dealing with <see cref="EventSubscription">EventSubscriptions</see>
    /// </summary>
    public interface IEventSubscriptionManager
    {
        /// <summary>
        /// Get all <see cref="EventSubscription">EventSubscriptions</see>
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see></returns>
        IEnumerable<EventSubscription> GetAllSubscriptions();

        /// <summary>
        /// Get all available <see cref="EventSubscription">EventSubscriptions</see>
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see></returns>
        IEnumerable<EventSubscription> GetAvailableSubscriptions();

        /// <summary>
        /// Process a set of <see cref="IEvent">Events</see> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process events for</param>
        /// <param name="events"><see cref="IEvent">Events</see> to process</param>
        void Process(EventSubscription subscription, IEnumerable<IEvent> events);

        /// <summary>
        /// Process a single <see cref="IEvent"/> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process event for</param>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        void Process(EventSubscription subscription, IEvent @event);

        /// <summary>
        /// Process a set of <see cref="IEvent">Events</see>
        /// </summary>
        /// <param name="events"><see cref="IEvent">Events</see> to process</param>
        void Process(IEnumerable<IEvent> events);

        /// <summary>
        /// Process a single <see cref="IEvent"/>
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        void Process(IEvent @event);
    }
}
