/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents a mapping for a specific property on a type
    /// </summary>
    public class PropertyMap<TSource, TTarget> : IPropertyMap
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PropertyMap"/>
        /// </summary>
        /// <param name="from"></param>
        public PropertyMap(PropertyInfo from)
        {
            From = from;
        }

#pragma warning disable 1591 // Xml Comments
        public PropertyInfo From { get; private set; }

        public IPropertyMappingStrategy Strategy { get; set; }
#pragma warning restore 1591 // Xml Comments
    }

}
