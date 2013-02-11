#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
        /// Adds properties from a given type and optionally exluding properties from a given type that is found in the inheritance chain
        /// </summary>
        /// <param name="container"><see cref="Container"/> to add properties to</param>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <param name="excludePropertiesFrom">Optional <see cref="Type"/> to use as basis for excluding properties</param>
        /// <param name="assignmentVisitor">Optional <see cref="Action{Assignment}">visitor</see> that gets called for every assignment for any property</param>
        /// <returns><see cref="Container"/> to keep building on</returns>
        public static Container WithPropertiesFrom(this Container container, Type type, Type excludePropertiesFrom = null, Action<Assignment> assignmentVisitor = null)
        {
            var properties = type.GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

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
                    assignment.WithObservableArray();
                else if (property.IsObservable())
                    assignment.WithObservable();
                else
                {
                    var objectLiteral = new ObjectLiteral();
                    assignment.Value = objectLiteral;
                    AddPropertiesFromType(objectLiteral, property.PropertyType.GetProperties(), assignmentVisitor);
                }

                if (assignmentVisitor != null) assignmentVisitor(assignment);

                parent.AddChild(assignment);

            }
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

        static bool IsObservable(this PropertyInfo property)
        {
            return property.PropertyType.IsValueType ||
                    property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(Type) ||
                    property.PropertyType == typeof(MethodInfo) ||
                    property.PropertyType == typeof(Guid) ||
                    property.PropertyType.IsNullable() ||
                    property.PropertyType.IsConcept();
        }
    }
}
