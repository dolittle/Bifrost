/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationResourceIdentifier"/>
    /// from a string and its not possible to find the <see cref="IApplicationResource"/> in the <see cref="string"/>
    /// </summary>
    public class MissingApplicationResource : ArgumentException
    {
        /// <summary>
        /// Initializes a new 
        /// </summary>
        public MissingApplicationResource(string identifierString)
            : base($"Missing application resource in '{identifierString}'. Expected format : {ApplicationResourceIdentifierConverter.ExpectedFormat}")
        { }
    }
}
