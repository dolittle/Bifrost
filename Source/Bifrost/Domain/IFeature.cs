/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a <see cref="IFeature"/> of a system
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// Gets the <see cref="FeatureName">name</see> of the <see cref="Feature"/>
        /// </summary>
        FeatureName Name { get; }

        /// <summary>
        /// Gets the <see cref="Module"/> this <see cref="Feature"/> belongs to
        /// </summary>
        IModule Module { get; }
        /// <summary>
        /// Gets the <see cref="SubFeature">sub features</see> for the <see cref="Feature"/>
        /// </summary>
        IEnumerable<ISubFeature> SubFeatures { get; }

        /// <summary>
        /// Add a <see cref="SubFeature"/> 
        /// </summary>
        /// <param name="subFeature"><see cref="SubFeature"/> to add</param>
        void AddSubFeature(ISubFeature subFeature);

    }
}
