/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Gets thrown when its impossible to identify an <see cref="IApplicationResource"/>
    /// </summary>
    public class UnableToIdentifyApplicationResource : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnableToIdentifyApplicationResource"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that is not possible to identify</param>
        public UnableToIdentifyApplicationResource(Type type) : base($"Unable to identify application resource for type '{type.FullName}'") { }

        /// <summary>
        /// Initializes a new instance of <see cref="UnableToIdentifyApplicationResource"/>
        /// </summary>
        /// <param name="identifierString"><see cref="string"/> that is not possible to identify</param>
        public UnableToIdentifyApplicationResource(string identifierString) : base($"Unable to identify application resource for string '{identifierString}'. Expected format should be: {ApplicationResourceIdentifierConverter.ExpectedFormat}") { }
    }
}
