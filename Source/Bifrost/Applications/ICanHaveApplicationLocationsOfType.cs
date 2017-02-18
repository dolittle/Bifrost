/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines something that can have <see cref="IApplicationLocation">application location</see>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICanHaveApplicationLocationsOfType<T>
    {
        /// <summary>
        /// Gets the children <see cref="IEnumerable{IApplicationLocation}">locations</see>
        /// </summary>
        IEnumerable<T> Children { get; }
    }
}
