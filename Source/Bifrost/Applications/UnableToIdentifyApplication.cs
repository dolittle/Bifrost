/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationResourceIdentifier"/>
    /// from a string and its not possible to identify the <see cref="IApplication"/> in the <see cref="string"/>
    /// </summary>
    public class UnableToIdentifyApplication : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnableToIdentifyApplication"/>
        /// </summary>
        /// <param name="identifierString">The invalid <see cref="string"/></param>
        public UnableToIdentifyApplication(string identifierString)
            : base($"Unable to identify application in '{identifierString}'. Expected format : {ApplicationResourceIdentifierConverter.ExpectedFormat}")
        { }
    }
}
