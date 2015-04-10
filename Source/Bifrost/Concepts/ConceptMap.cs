#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Collections.Concurrent;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Maps a concept type to the underlying primitive type
    /// </summary>
    public static class ConceptMap
    {
        static readonly ConcurrentDictionary<Type, Type> _cache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// Get the type of the value in a <see cref="ConceptAs{T}"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get value type from</param>
        /// <returns>The type of the <see cref="ConceptAs{T}"/> value</returns>
        public static Type GetConceptValueType(Type type)
        {
            return _cache.GetOrAdd(type, GetPrimitiveType);
        }

        static Type GetPrimitiveType(Type type)
        {
            var conceptType = FindConceptAsType(type);
#if(NETFX_CORE)
            var typeInfo = conceptType.GetTypeInfo();
            if (conceptType == null || !typeInfo.IsGenericType) return null;
            var genericArgumentType = typeInfo.GenericTypeArguments[0];
#else
            if (conceptType == null || !conceptType.IsGenericType) return null;
            var genericArgumentType = conceptType.GetGenericArguments()[0];
#endif
            return genericArgumentType.HasInterface(typeof(IEquatable<>)) ? genericArgumentType : null;
        }

        static Type FindConceptAsType(Type type)
        {
            var openGenericType = typeof (ConceptAs<>);
            var typeToCheck = type;
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
#if(NETFX_CORE)
                var currentType = typeToCheck.GetTypeInfo().IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
#else
                var currentType = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
#endif
                if (openGenericType == currentType)
                {
                    return typeToCheck;
                }

#if(NETFX_CORE)
                typeToCheck = typeToCheck.GetTypeInfo().BaseType;
#else
                typeToCheck = typeToCheck.BaseType;
#endif
            }
            return null;
        }
    }
}