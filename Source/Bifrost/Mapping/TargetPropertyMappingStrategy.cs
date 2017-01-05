/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> that supports mapping to 
    /// a specified property
    /// </summary>
    public class TargetPropertyMappingStrategy : IPropertyMappingStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TargetPropertyMappingStrategy"/>
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> representing the property</param>
        public TargetPropertyMappingStrategy(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> representing the property
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public void Perform(IMappingTarget mappingTarget, object target, object sourceValue)
        {
            mappingTarget.SetValue(target, PropertyInfo, sourceValue);
        }
#pragma warning restore 1591 // Xml Comments

    }
}
