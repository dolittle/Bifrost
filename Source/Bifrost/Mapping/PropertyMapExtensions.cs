/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
using Bifrost.Extensions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Extends <see cref="PropertyMap"/> with mapping utilities
    /// </summary>
    public static class PropertyMapExtensions
    {
        /// <summary>
        /// Map a property to another property in the target
        /// </summary>
        /// <typeparam name="TSource">Type of the source to map from</typeparam>
        /// <typeparam name="TTarget">Type of the target to map to</typeparam>
        /// <param name="propertyMap">The <see cref="PropertyMap{TSource,TTarget}"/></param>
        /// <param name="propertyExpression">Expression representing the property</param>
        /// <returns>The propertymap for fluent interfaces</returns>
        public static PropertyMap<TSource, TTarget> To<TSource, TTarget>(this PropertyMap<TSource, TTarget> propertyMap, Expression<Func<TTarget, object>> propertyExpression)
        {
            var property = propertyExpression.GetPropertyInfo();
            propertyMap.Strategy = new TargetPropertyMappingStrategy(property);
            return propertyMap;
        }
    }
}
