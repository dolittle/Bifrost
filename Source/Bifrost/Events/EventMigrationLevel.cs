/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the migration level of an <see cref="IEvent"/>
    /// </summary>
    public class EventMigrationLevel : ConceptAs<int>
    {
        /// <summary>
        /// Implicitly convert from a <see cref="int"/> to an <see cref="EventMigrationLevel"/>
        /// </summary>
        /// <param name="level">The level</param>
        public static implicit operator EventMigrationLevel(int level)
        {
            return new EventMigrationLevel { Value = level };
        }
    }
}
