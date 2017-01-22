/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a module within a <see cref="IBoundedContext"/>
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the <see cref="ModuleName">name</see> of the <see cref="Module"/>
        /// </summary>
        ModuleName Name { get; }

        /// <summary>
        /// Gets the <see cref="BoundedContext"/> the feature belongs to
        /// </summary>
        IBoundedContext BoundedContext { get; }

        /// <summary>
        /// Gets all <see cref="Feature">features</see> within the <see cref="Module"/>
        /// </summary>
        IEnumerable<IFeature> Features { get; }

        /// <summary>
        /// Add a feature to the <see cref="Module"/>
        /// </summary>
        /// <param name="feature"></param>
        void AddFeature(IFeature feature);
    }
}
