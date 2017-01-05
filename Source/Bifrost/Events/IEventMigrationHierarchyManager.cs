/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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