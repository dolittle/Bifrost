/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Exception tat
    /// </summary>
    public class SubFeatureAlreadyAddedToFeature : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SubFeatureAlreadyAddedToFeature"/>
        /// </summary>
        /// <param name="feature"><see cref="IFeature">Feature</see> the sub feature is already added to</param>
        /// <param name="subFeature"><see cref="ISubFeature">Sub feature</see> that is already added</param>
        public SubFeatureAlreadyAddedToFeature(IFeature feature, ISubFeature subFeature) : base($"Feature '{subFeature.Name}' has already been added to the feature '{subFeature.Name}'") { }
    }
}
