#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
    /// Defines a system for working with <see cref="IEventSubscription">Event Subscriptions</see>
    /// </summary>
    public interface IEventSubscriptions
    {
        /// <summary>
        /// Get all subscriptions available
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see> </returns>
        IEnumerable<EventSubscription> GetAll();

        /// <summary>
        /// Save the state of an event subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to save</param>
        void Save(EventSubscription subscription);

        /// <summary>
        /// Reset last event id for all subscriptions
        /// </summary>
        void ResetLastEventForAllSubscriptions();
    }
}
