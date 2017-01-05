/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Mapping;

namespace Bifrost.Entities.Files.Mapping
{
    /// <summary>
    /// Provides extensions to <see cref="IPropertyMap"/>
    /// </summary>
    public static class PropertyMapExtensions
    {
        /// <summary>
        /// Defines a property as the key
        /// </summary>
        /// <param name="propertyMap"><see cref="IPropertyMap"/> to define as key</param>
        /// <returns>Chained <see cref="IPropertyMap"/></returns>
        public static IPropertyMap Key(this IPropertyMap propertyMap)
        {
            propertyMap.Strategy = new KeyStrategy();
            return propertyMap;
        }
    }
}
