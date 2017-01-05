/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// The exception that is thrown when a type is missing a default constructor and one is required
    /// </summary>
    public class CanCreateContainerNotFoundException : ArgumentException
    {
        /// <summary>
        /// Initializes an instance of <see cref="CanCreateContainerNotFoundException"/>
        /// </summary>
        public CanCreateContainerNotFoundException() : base("Couldn't discover an implementation of 'ICanCreateContainer'") { }
    }
}
