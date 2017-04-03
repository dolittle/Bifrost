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
    public class ModuleAlreadyAddedToBoundedContext : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FeatureAlreadyAddedToModule"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="IBoundedContext">Bounded context</see> the feature is already added to</param>
        /// <param name="module"><see cref="IModule">Module</see> hat is already added</param>
        public ModuleAlreadyAddedToBoundedContext(IBoundedContext boundedContext, IModule module) : base($"Module '{module.Name}' has already been added to the bounded context '{boundedContext.Name}'") { }
    }
}
