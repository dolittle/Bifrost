/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a system for working with <see cref="IMappingTarget">mapping targets</see>
    /// </summary>
    public interface IMappingTargets
    {
        /// <summary>
        /// Get <see cref="IMappingTarget"/> for a specified type
        /// </summary>
        /// <param name="type">Type to get <see cref="IMappingTarget"/> for</param>
        /// <returns>The <see cref="IMappingTarget"/> for the type</returns>
        IMappingTarget GetFor(Type type);
    }
}
