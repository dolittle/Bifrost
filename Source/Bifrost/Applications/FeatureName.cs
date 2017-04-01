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
    public class FeatureName : ConceptAs<string>, IApplicationLocationName
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return this;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="FeatureName"/>
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        public static implicit operator FeatureName(string featureName)
        {
            return new FeatureName { Value = featureName };
        }
    }
}
