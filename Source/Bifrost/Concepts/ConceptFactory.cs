/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Factory to create an instance of an<see cref="ConceptAs"/> from the Type and Underlying value.
    /// </summary>
    public class ConceptFactory
    {
        /// <summary>
        /// Creates an instance of a <see cref="ConceptAs"/> given the type and underlying value.
        /// </summary>
        /// <param name="type">Type of the ConceptAs to create</param>
        /// <param name="value">Value to give to this instance</param>
        /// <returns>An instance of a ConceptAs with the specified value</returns>
        public static object CreateConceptInstance(Type type, object value)
        {
            var instance = Activator.CreateInstance(type);
            var val = new object();

            var valueProperty = type.GetTypeInfo().GetProperty("Value");

            var genericArgumentType = GetPrimitiveTypeConceptIsBasedOn(type);
            if (genericArgumentType == typeof (Guid))
            {
                val = value == null ? Guid.Empty : Guid.Parse(value.ToString());
            }

            if (IsPrimitive(valueProperty.PropertyType))
            {
                val = value ?? Activator.CreateInstance(valueProperty.PropertyType);
            }

            if (valueProperty.PropertyType == typeof (string))
            {
                val = value ?? string.Empty;
            }

            if (valueProperty.PropertyType == typeof (DateTime))
            {
                val = value ?? default(DateTime);
            }

            if (val.GetType() != genericArgumentType)
                val = Convert.ChangeType(val, genericArgumentType, null);

            valueProperty.SetValue(instance, val, null);

            return instance;
        }

        static Type GetPrimitiveTypeConceptIsBasedOn(Type conceptType)
        {
            return ConceptMap.GetConceptValueType(conceptType);
        }
        static bool IsPrimitive(Type type)
        {
            return type.GetTypeInfo().IsPrimitive || type == typeof(decimal);
        }
    }
}