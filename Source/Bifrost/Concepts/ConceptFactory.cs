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
#if(NETFX_CORE)

#else
            var genericArgumentType = type.BaseType.GetGenericArguments()[0];
            if (genericArgumentType == typeof(Guid))
                value = Guid.Parse(value.ToString());

            type.GetProperty("Value").SetValue(instance, value, null);
#endif
            return instance;
        }
    }
}