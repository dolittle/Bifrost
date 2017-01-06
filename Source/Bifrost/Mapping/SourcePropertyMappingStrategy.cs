/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents a <see cref="IPropertyMappingStrategy"/> that is typically used when property should map to self - same property as source
    /// </summary>
    /// <remarks>
    /// If the property does not exist in the target, it will just ignore it and the value won't be set. It does not qualify to be an exceptional state.
    /// </remarks>
    public class SourcePropertyMappingStrategy : IPropertyMappingStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SourcePropertyMappingStrategy"/>
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> to base it from</param>
        public SourcePropertyMappingStrategy(PropertyInfo propertyInfo)
        {
        }

#pragma warning disable 1591 // Xml Comments
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {

        }
#pragma warning restore 1591 // Xml Comments
    }
}
