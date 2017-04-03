/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ISubFeature"/>
    /// </summary>
    public class SubFeature : ISubFeature
    {
        List<ISubFeature> _subFeatures = new List<ISubFeature>();

        /// <summary>
        /// Initializes a new instance of <see cref="SubFeature"/>
        /// </summary>
        /// <param name="parent">Parent <see cref="Feature"/></param>
        /// <param name="featureName">Name of the <see cref="IFeature"/></param>
        public SubFeature(IFeature parent, FeatureName featureName)
        {
            Module = parent.Parent;
            Name = featureName;
            Parent = parent;
            parent.AddSubFeature(this);
        }

        /// <inheritdoc/>
        public IModule Module { get; }

        /// <inheritdoc/>
        public FeatureName Name { get; }

        IModule IBelongToAnApplicationLocationTypeOf<IModule>.Parent { get; }

        /// <inheritdoc/>
        public IFeature Parent { get; }

        /// <inheritdoc/>
        public IEnumerable<ISubFeature> Children => _subFeatures;

        /// <inheritdoc/>
        public void AddSubFeature(ISubFeature subFeature)
        {
            ThrowIfSubFeatureAlradyAdded(subFeature);
            _subFeatures.Add(subFeature);
        }

        void ThrowIfSubFeatureAlradyAdded(ISubFeature subFeature)
        {
            if (_subFeatures.Contains(subFeature)) throw new SubFeatureAlreadyAddedToFeature(this, subFeature);
        }

        IApplicationLocationName IApplicationLocation.Name => Name;
    }
}
