#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Extensions;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Concepts
{
    /// <summary>
    /// Provides extensions related to <see cref="Type">types</see> and others related to <see cref="ConceptAs{T}"/>
    /// </summary>
    public static class ConceptExtensions
    {
        /// <summary>
        /// Check if a type is a concept or not
        /// </summary>
        /// <param name="objectType"><see cref="Type"/> to check</param>
        /// <returns>True if type is a concept, false if not</returns>
        public static bool IsConcept(this Type objectType)
        {
            return objectType.IsDerivedFromOpenGeneric(typeof (ConceptAs<>));
        }

        /// <summary>
        /// Check if an object is an instance of a concept or not
        /// </summary>
        /// <param name="instance">instance to check</param>
        /// <returns>True if object is a concept, false if not</returns>
        public static bool IsConcept(this Object instance)
        {
            return IsConcept(instance.GetType());
        }

        /// <summary>
        /// Get the type of the value inside a <see cref="ConceptAs{T}"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get value type from</param>
        /// <returns>The type of the <see cref="ConceptAs{T}"/> value</returns>
        public static Type GetConceptValueType(this Type type)
        {
#if(NETFX_CORE)
            var baseType = type.GetTypeInfo().BaseType.GetTypeInfo();
            if (baseType != null && baseType.IsGenericType)
            {
                var genericArgumentType = baseType.GenericTypeArguments[0];
                if (genericArgumentType.HasInterface(typeof(IEquatable<>)))
                    return genericArgumentType;
            }
#else
            var baseType = type.BaseType;
            if (baseType != null && baseType.IsGenericType)
            {
                var genericArgumentType = baseType.GetGenericArguments()[0];
                if (genericArgumentType.HasInterface(typeof(IEquatable<>)))
                    return genericArgumentType;
            }
#endif
            return null;
        }

        /// <summary>
        /// Takes a Concept{T} value object as an object and returns the correct primitive value, also as an object
        /// </summary>
        /// <param name="conceptObject">The concept as an object</param>
        /// <returns>The value of the primitive type on which the concept is based</returns>
        public static object GetConceptValue(this object conceptObject)
        {
            if (!IsConcept(conceptObject))
                throw new ArgumentException("The object is not a ConceptAs<>.  You can check if a type or an instance is a Concept through the 'IsConcept' method.");

            return ((dynamic)conceptObject).Value;
        }
    }
}
