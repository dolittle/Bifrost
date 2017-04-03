/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationResourceIdentifier"/>
    /// from a string and its not possible to find the <see cref="IApplicationResourceType"/> in the <see cref="string"/>
    /// </summary>
    public class MissingApplicationResourceType : ArgumentException
    {
        /// <summary>
        /// Initializes a new 
        /// </summary>
        public MissingApplicationResourceType(string identifierString)
            : base($"Missing application resource type in '{identifierString}'. Expected format : {ApplicationResourceIdentifierConverter.ExpectedFormat}")
        { }
    }
}
