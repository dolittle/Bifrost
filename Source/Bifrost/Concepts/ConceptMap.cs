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
#if(NETFX_CORE)
            var typeProperty = type.GetTypeInfo().GetRuntimeProperty("UnderlyingType");
#else
            var typeProperty = type.GetProperty("UnderlyingType",
                BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public);
#endif
            return typeProperty != null ? (Type) typeProperty.GetValue(null) : null;
        }
    }
}