/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the generation of an <see cref="IEvent"/>
    /// </summary>
    public class EventGeneration : ConceptAs<int>
    {
        /// <summary>
        /// First generation of an event
        /// </summary>
        public static EventGeneration First = 1;

        /// <summary>
        /// Implicitly convert from a <see cref="int"/> to an <see cref="EventGeneration"/>
        /// </summary>
        /// <param name="generation">The generation</param>
        public static implicit operator EventGeneration(int generation)
        {
            return new EventGeneration { Value = generation };
        }
    }
}
