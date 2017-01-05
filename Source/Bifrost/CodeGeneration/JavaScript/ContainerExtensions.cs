/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with <see cref="Container"/>
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds properties as observables from a given type and optionally exluding properties from a given type that is found in the inheritance chain
        /// </summary>
        /// <param name="container"><see cref="Container"/> to add properties to</param>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <param name="excludePropertiesFrom">Optional <see cref="Type"/> to use as basis for excluding properties</param>
        /// <param name="propertyVisitor">Optional visitor that gets called for every property - return false will ignore the property</param>
        /// <param name="assignmentVisitor">Optional <see cref="Action{Assignment}">visitor</see> that gets called for every assignment for any property</param>
        /// <param name="observableVisitor">Optional <see cref="Action{Observable}">visitor</see> that gets called for every observable property</param>
        /// <returns><see cref="Container"/> to keep building on</returns>
        public static Container WithObservablePropertiesFrom(this Container container, Type type, Type excludePropertiesFrom = null, Func<PropertyInfo, bool> propertyVisitor = null, Action<Assignment> assignmentVisitor = null, ObservableVisitor observableVisitor = null)
        {
            var properties = type.GetTypeInfo().GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetTypeInfo().GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

            if (propertyVisitor != null)
                properties = properties.Where(propertyVisitor).ToArray();

            AddObservablePropertiesFromType(container, properties, assignmentVisitor, observableVisitor);

            return container;
        }


        /// <summary>
        /// Adds properties from a given type and optionally exluding properties from a given type that is found in the inheritance chain
        /// </summary>
        /// <param name="container"><see cref="Container"/> to add properties to</param>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <param name="excludePropertiesFrom">Optional <see cref="Type"/> to use as basis for excluding properties</param>
        /// <param name="assignmentVisitor">Optional <see cref="Action{Assignment}">visitor</see> that gets called for every assignment for any property</param>
        /// <returns><see cref="Container"/> to keep building on</returns>
        public static Container WithPropertiesFrom(this Container container, Type type, Type excludePropertiesFrom = null, Action<Assignment> assignmentVisitor = null)
        {
            var properties = type.GetTypeInfo().GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetTypeInfo().GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

            AddPropertiesFromType(container, properties, assignmentVisitor);

            return container;
        }


        static void AddPropertiesFromType(Container parent, IEnumerable<PropertyInfo> properties, Action<Assignment> assignmentVisitor)
        {
            foreach (var property in properties)
            {
                var propertyName = property.Name.ToCamelCase();

                Assignment assignment;
                if (parent is FunctionBody)
                    assignment = new PropertyAssignment(propertyName);
                else
                    assignment = new KeyAssignment(propertyName);

                if (property.IsDictionary())
                    assignment.WithObjectLiteral();
                else if (property.IsEnumerable())
                    assignment.WithEmptyArray();
                else if (property.PropertyType.IsConcept())
                    assignment.WithDefaultValue(property.PropertyType.GetConceptValueType());
                else if (property.PropertyType.IsNullable())
                    assignment.WithNullValue();
                else if (property.IsDateTime())
                    assignment.WithDate();
                else if (property.IsBoolean())
                    assignment.WithBoolean();
                else if (property.IsEnum())
                    assignment.WithDefaultEnumValue(property.PropertyType);
                else if (property.PropertyType.IsNumericType())
                    assignment.WithDefaultNumericValue(property.PropertyType);
                else if (property.HasPrimitiveDefaultValue())
                    assignment.WithDefaultValue(property.PropertyType);
                else
                {
                    var objectLiteral = new ObjectLiteral();
                    assignment.Value = objectLiteral;
                    AddPropertiesFromType(objectLiteral, property.PropertyType.GetTypeInfo().GetProperties(), assignmentVisitor);
                }

                if (assignmentVisitor != null) assignmentVisitor(assignment);

                parent.AddChild(assignment);

            }
        }

        static void AddObservablePropertiesFromType(Container parent, IEnumerable<PropertyInfo> properties, Action<Assignment> assignmentVisitor, ObservableVisitor observableVisitor)
        {
            foreach (var property in properties)
            {
                var propertyName = property.Name.ToCamelCase();

                Assignment assignment;
                if (parent is FunctionBody)
                    assignment = new PropertyAssignment(propertyName);
                else
                    assignment = new KeyAssignment(propertyName);

                if (property.IsDictionary())
                    assignment.WithObjectLiteral();
                else if (property.IsEnumerable())
                    assignment.WithObservableArray();
                else if (property.IsObservable())
                    assignment.WithObservable(observableVisitor);
                else
                {
                    var objectLiteral = new ObjectLiteral();
                    assignment.Value = objectLiteral;
                    AddObservablePropertiesFromType(objectLiteral, property.PropertyType.GetTypeInfo().GetProperties(), assignmentVisitor, observableVisitor);
                }

                if (assignmentVisitor != null) assignmentVisitor(assignment);

                parent.AddChild(assignment);

            }
        }


        static bool IsEnum(this PropertyInfo property)
        {
            return property.PropertyType.GetTypeInfo().IsEnum;
        }

        static bool IsEnumerable(this PropertyInfo property)
        {
            return (property.PropertyType.HasInterface(typeof(IEnumerable<>)) ||
                   property.PropertyType.HasInterface<IEnumerable>()) && property.PropertyType != typeof(string);
        }

        static bool IsDictionary(this PropertyInfo property)
        {
            return property.PropertyType.HasInterface(typeof(IDictionary<,>)) ||
                    property.PropertyType.HasInterface<IDictionary>();
        }

        static bool IsDateTime(this PropertyInfo property)
        {
            return property.PropertyType == typeof(DateTime);
        }

        static bool IsBoolean(this PropertyInfo property)
        {
            return property.PropertyType == typeof(bool);
        }

        static bool HasPrimitiveDefaultValue(this PropertyInfo property)
        {
            return property.PropertyType.GetTypeInfo().IsValueType ||
                    property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(Type) ||
                    property.PropertyType == typeof(MethodInfo) ||
                    property.PropertyType == typeof(Guid);
        }

        static bool IsObservable(this PropertyInfo property)
        {
            return property.PropertyType.GetTypeInfo().IsValueType ||
                    property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(Type) ||
                    property.PropertyType == typeof(MethodInfo) ||
                    property.PropertyType == typeof(Guid) ||
                    property.PropertyType.IsNullable() ||
                    property.PropertyType.IsConcept();
        }
    }
}
