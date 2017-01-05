/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines the functionality for a manager that handles the migration of events from older generations to newer generations.
    ///
    /// Migrates an <see cref="IEvent">Event</see> to the current generation
    /// </summary>
    public interface IEventMigratorManager
    {
        /// <summary>
        /// Migrates an event from the particular generation to the current generation of the event
        /// </summary>
        /// <param name="source">A previous generation of the event</param>
        /// <returns>The current generation of the event</returns>
        IEvent Migrate(IEvent source);
    }
}