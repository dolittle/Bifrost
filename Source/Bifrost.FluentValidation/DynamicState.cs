/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Represents state used
    /// </summary>
    public class DynamicState : DynamicObject
    {
        object _model;
        List<PropertyInfo> _properties = new List<PropertyInfo>();

        /// <summary>
        /// Initializes a new instance of <see cref="DynamicState"/>
        /// </summary>
        /// <param name="model">Model to use as base for representing the state</param>
        public DynamicState(object model)
        {
            _model = model;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DynamicState"/>
        /// </summary>
        /// <param name="model">Model to use as base for representing the state</param>
        /// <param name="properties">Properties that are supported</param>
        public DynamicState(object model, IEnumerable<PropertyInfo> properties)
        {
            _model = model;
            _properties.AddRange(properties);
        }

#pragma warning disable 1591 // Xml Comments
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            foreach (var property in _properties)
            {
                if (property.Name.Equals(binder.Name))
                {
                    var actualModel = GetActualModel(_model, property.DeclaringType);
                    result = property.GetValue(actualModel, null);
                    return true;
                }
            }

            result = null;
            return false;
        }
#pragma warning restore 1591 // Xml Comments

        object GetActualModel(object model, Type targetType)
        {
            var modelType = model.GetType();
            if (modelType == targetType)
                return model;

            var properties = modelType.GetTypeInfo().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(model, null);
                if (property.PropertyType == targetType)
                    return propertyValue;
                else
                {
                    var result = GetActualModel(propertyValue, targetType);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }
    }
}
