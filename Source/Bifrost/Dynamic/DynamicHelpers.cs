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
using System.Dynamic;
using System.ComponentModel;
using System.Collections.Generic;

namespace Bifrost.Dynamic
{
    /// <summary>
    /// Provides a set of helper methods for working with dynamic types
    /// </summary>
    public class DynamicHelpers
    {
        /// <summary>
        /// Populate a dynamic object, typically something like a <see cref="System.Dynamic.ExpandoObject"/>
        /// </summary>
        /// <param name="target">Target object that will receive all properties and values from source</param>
        /// <param name="source">Source object containing all properties with values - this can in fact be any type, including an anonymous one</param>
        public static void Populate(dynamic target, dynamic source)
        {
            var dictionary = target as IDictionary<string, object>;

            foreach (var property in source.GetType().GetProperties())
                dictionary[property.Name] = property.GetValue(source, null);
        }
    }
}
