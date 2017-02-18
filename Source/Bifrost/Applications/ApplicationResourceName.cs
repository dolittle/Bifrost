/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents the name of a <see cref="IApplicationResource"/> 
    /// </summary>
    public class ApplicationResourceName : ConceptAs<string>, IApplicationResourceName
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return this;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="ApplicationResourceName"/>
        /// </summary>
        /// <param name="applicationResourceName">Name of the <see cref="ApplicationResourceName"/></param>
        public static implicit operator ApplicationResourceName(string applicationResourceName)
        {
            return new ApplicationResourceName { Value = applicationResourceName };
        }
    }
}
