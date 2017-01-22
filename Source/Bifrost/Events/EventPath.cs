/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the concept of a path for an event within the application structure
    /// </summary>
    public class EventPath : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="EventPath"/>
        /// </summary>
        /// <param name="path"><see cref="string">Actual path</see></param>
        public static implicit operator EventPath(string path)
        {
            return new EventPath { Value = path };
        }
    }
}
