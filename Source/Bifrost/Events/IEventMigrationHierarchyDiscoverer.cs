/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a discoverer that is able to detect all events and group them into <see cref="EventMigrationHierarchy">EventMigrationHierarchies</see>
    /// </summary>
    public interface IEventMigrationHierarchyDiscoverer
    {
        /// <summary>
        /// Gets all the <see cref="EventMigrationHierarchy">EventMigrationHierarchies</see> for events
        /// </summary>
        /// <returns>Event Migration Hierarchies</returns>
        IEnumerable<EventMigrationHierarchy> GetMigrationHierarchies();
    }
}