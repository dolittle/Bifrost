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
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a manager that is able to determine what the current migration level for an <see cref="IEvent">Event</see>
    /// and what the <see cref="Type">concrete type</see> of any specified migration level.
    /// </summary>
    public interface IEventMigrationHierarchyManager
    {
        /// <summary>
        /// Gets the number of generations (migrations) that the logical event has gone through.
        /// </summary>
        /// <param name="logicalEvent">The logical event (initial generation)</param>
        /// <returns>migration level</returns>
        int GetCurrentMigrationLevelForLogicalEvent(Type logicalEvent);

        /// <summary>
        /// Gets the concrete type that the logical event took at the specified migration level
        /// </summary>
        /// <param name="logicalEvent">The logical event</param>
        /// <param name="level">The level we wish the concrete type for</param>
        /// <returns>The concrete type</returns>
        Type GetConcreteTypeForLogicalEventMigrationLevel(Type logicalEvent, int level);

        /// <summary>
        /// Gets the logical event type of the migration hierarchy of which the passed in event is part
        /// </summary>
        /// <param name="event">Event for which you want to know the logical event </param>
        /// <returns>Type of the logical event</returns>
        Type GetLogicalTypeForEvent(Type @event);


        /// <summary>
        /// Gets the logical event type of the migration hierarchy from the name of the logical event
        /// </summary>
        /// <param name="logicalEventName">Name of the logical event</param>
        /// <returns>Type of the logical event</returns>
        Type GetLogicalTypeFromName(string logicalEventName);
    }
}