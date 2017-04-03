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
    public class FeatureAlreadyAddedToModule : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FeatureAlreadyAddedToModule"/>
        /// </summary>
        /// <param name="module"><see cref="IModule">Module</see> feature is already added to</param>
        /// <param name="feature"><see cref="IFeature">Feature</see> that is already added</param>
        public FeatureAlreadyAddedToModule(IModule module, IFeature feature) : base($"Feature '{feature.Name}' has already been added to the module '{module.Name}'") { }
    }
}
