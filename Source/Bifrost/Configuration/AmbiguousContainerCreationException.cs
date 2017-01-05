/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// The exception that is thrown when a ambiguous match is found when discovering implementations of <see cref="ICanCreateContainer"/>
    /// </summary>
    public class AmbiguousContainerCreationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AmbiguousContainerCreationException"/>
        /// </summary>
        public AmbiguousContainerCreationException() : base("Multiple implementations of ICanCreateContainer was found") { }
    }
}
