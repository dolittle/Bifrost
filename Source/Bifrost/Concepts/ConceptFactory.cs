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
#if(NETFX_CORE)
using System.Reflection;
#endif
using Bifrost.Extensions;

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
#if(NETFX_CORE)

#else
            var valueProperty = type.GetProperty("Value");

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
#endif
            return instance;
        }

        static Type GetPrimitiveTypeConceptIsBasedOn(Type conceptType)
        {
            return ConceptMap.GetConceptValueType(conceptType);
        }
        static bool IsPrimitive(Type type)
        {
#if(NETFX_CORE)
            return type.GetTypeInfo().IsPrimitive || type == typeof(decimal);
#else
            return type.IsPrimitive || type == typeof(decimal);
#endif
        }
    }
}