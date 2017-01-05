/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a system for knowing about maps
    /// </summary>
    public interface IMaps
    {
        /// <summary>
        /// Check if there is a map for the given combination of source and target
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see></param>
        /// <param name="target"><see cref="Type">Target type</see></param>
        /// <returns>true if there is a map, false if not</returns>
        bool HasFor(Type source, Type target);

        /// <summary>
        /// Get a map for specific source and target types
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see></param>
        /// <param name="target"><see cref="Type">Target type</see></param>
        /// <returns><see cref="IMap"/> for the combination</returns>
        IMap GetFor(Type source, Type target);
    }
}
