/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines something can deal with creating proxy types
    /// </summary>
    public interface IProxying
    {
        /// <summary>
        /// Build an interface type that contains the properties from a specific other type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <returns>A new <see cref="Type"/></returns>
        Type BuildInterfaceWithPropertiesFrom(Type type);

        /// <summary>
        /// Build a class type that contains the properties from a specific other type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <returns>A new <see cref="Type"/></returns>
        Type BuildClassWithPropertiesFrom(Type type);
    }
}
