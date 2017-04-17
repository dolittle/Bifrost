/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
using Bifrost.Extensions;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bifrost.Events.Azure.Tables
{

    /// <summary>
    /// Represents helper methods for working with property conversion related to Azure Tables and entities
    /// </summary>
    /// <typeparam name="TTarget">Target object <see cref="Type"/></typeparam>
    public class PropertiesFor<TTarget>
    {
        /// <summary>
        /// Get a value from a <see cref="DynamicTableEntity"/> by giving the target objects property.
        /// From the expression given it will find the expected type and also the name of the property to
        /// look for in the entity from Azure Tables
        /// </summary>
        /// <typeparam name="TProperty"><see cref="Type"/> of property type - inferred</typeparam>
        /// <param name="entity"><see cref="DynamicTableEntity">Entity</see> to get from</param>
        /// <param name="property">Property expression</param>
        /// <returns>Value of the property from the <see cref="DynamicTableEntity">entity</see></returns>
        public static TProperty GetValue<TProperty>(DynamicTableEntity entity, Expression<Func<TTarget, TProperty>> property)
        {
            var propertyInfo = property.GetPropertyInfo();
            return (TProperty)PropertyHelper.GetValue(entity, propertyInfo);
        }

        /// <summary>
        /// Get propertyname from a property expression
        /// </summary>
        /// <param name="property">Property expression</param>
        /// <returns>Name of the property</returns>
        public static string GetPropertyName(Expression<Func<TTarget, object>> property)
        {
            var propertyInfo = property.GetPropertyInfo();
            return propertyInfo.Name;
        }
    }
}
