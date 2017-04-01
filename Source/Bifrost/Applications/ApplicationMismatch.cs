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
    public class ApplicationMismatch : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationMismatch"/>
        /// </summary>
        public ApplicationMismatch(string applicationName, string identifierString)
            : base($"Application mismatch in '{identifierString}'. Expected application name '{applicationName}. Format of a string is expected to be '{ApplicationResourceIdentifierConverter.ExpectedFormat}")
        { }
    }
}
