/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bifrost.Events.Azure.Tables
{
    /// <summary>
    /// Represents a helper for working with properties
    /// </summary>
    public class PropertyHelper
    {
        /// <summary>
        /// Get a value from a <see cref="DynamicTableEntity"/> by giving a property info representing the property to get value for
        /// </summary>
        /// <param name="entity"><see cref="DynamicTableEntity">Entity</see> to get from</param>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> for the property</param>
        /// <returns>Value of the property from the <see cref="DynamicTableEntity">entity</see></returns>
        public static object GetValue(DynamicTableEntity entity, PropertyInfo propertyInfo)
        {
            var valueType = propertyInfo.PropertyType;
            var entityProperty = entity.Properties[propertyInfo.Name];
            var concept = valueType.IsConcept();

            object value = null;
            if (concept) valueType = valueType.GetConceptValueType();

            if (valueType == typeof(EventSourceVersion)) value = EventSourceVersion.FromCombined(entityProperty.DoubleValue.Value);
            if (valueType == typeof(Guid)) value = entityProperty.GuidValue.Value;
            if (valueType == typeof(int)) value = entityProperty.Int32Value.Value;
            if (valueType == typeof(long)) value = entityProperty.Int64Value.Value;
            if (valueType == typeof(string)) value = entityProperty.StringValue;
            if (valueType == typeof(DateTime)) value = entityProperty.DateTime.Value;
            if (valueType == typeof(DateTimeOffset)) value = entityProperty.DateTimeOffsetValue.Value;
            if (valueType == typeof(bool)) value = entityProperty.BooleanValue.Value;
            if (valueType == typeof(double)) value = entityProperty.DoubleValue.Value;
            if (valueType == typeof(float)) value = (float)entityProperty.DoubleValue.Value;

            if (concept) return ConceptFactory.CreateConceptInstance(propertyInfo.PropertyType, value);

            return value;
        }
    }
}
