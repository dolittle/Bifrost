/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationResourceIdentifier"/>
    /// from a string and its not possible to find the <see cref="IApplicationLocation">application location</see> in the <see cref="string"/>
    /// </summary>
    public class MissingApplicationLocations : ArgumentException
    {
        /// <summary>
        /// Initializes a new 
        /// </summary>
        public MissingApplicationLocations(string identifierString)
            : base($"Missing location in '{identifierString}'. Expected format : {ApplicationResourceIdentifierConverter.ExpectedFormat}")
        { }
    }
}
