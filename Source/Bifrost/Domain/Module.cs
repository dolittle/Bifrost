/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represent an implementation of <see cref="IModule"/>
    /// </summary>
    public class Module : IModule
    {
        List<IFeature> _features = new List<IFeature>();

        /// <summary>
        /// Initializes a new instance of <see cref="Module"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="IBoundedContext"/> the <see cref="Module"/> belongs to</param>
        /// <param name="moduleName"><see cref="ModuleName">Name</see> of the business component</param>
        public Module(IBoundedContext boundedContext, ModuleName moduleName)
        {
            BoundedContext = boundedContext;
            Name = moduleName;
            boundedContext.AddModule(this);
        }

        /// <inheritdoc/>
        public ModuleName Name { get; }

        /// <inheritdoc/>
        public IBoundedContext   BoundedContext { get; }

        /// <inheritdoc/>
        public IEnumerable<IFeature> Features => _features;

        /// <inheritdoc/>
        public void AddFeature(IFeature feature)
        {
            ThrowIfFeatureIsAlreadyAdded(feature);
            _features.Add(feature);
        }

        void ThrowIfFeatureIsAlreadyAdded(IFeature feature)
        {
            if (_features.Contains(feature)) throw new FeatureAlreadyAddedToModule(this, feature);
        }
    }
}
