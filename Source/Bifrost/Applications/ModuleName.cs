/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents the name of a <see cref="Feature"/>
    /// </summary>
    public class ModuleName : ConceptAs<string>, IApplicationLocationName
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return this;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="ModuleName"/>
        /// </summary>
        /// <param name="businessComponentName">Name of the business component</param>
        public static implicit operator ModuleName(string businessComponentName)
        {
            return new ModuleName { Value = businessComponentName };
        }
    }
}
