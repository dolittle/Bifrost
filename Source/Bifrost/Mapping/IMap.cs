﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Conventions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a map that describes mapping for an object.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered.
    /// You most likely want to subclass <see cref="Map{TSource,TTarget}"/>.
    /// </remarks>
    public interface IMap : IConvention
    {
        /// <summary>
        /// Gets the source type the map is for
        /// </summary>
        Type Source { get;  }

        /// <summary>
        /// Gets the target type the map is for
        /// </summary>
        Type Target { get; }

        /// <summary>
        /// Get the mapped properties
        /// </summary>
        IEnumerable<IPropertyMap> Properties { get; }
    }
}
