#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;
using System.Dynamic;

namespace Bifrost.Dynamic
{
    /// <summary>
    /// Provides a set of extension methods for working with dynamic types
    /// </summary>
    public static class DynamicExtensions
    {
        /// <summary>
        /// Converts an object to a dynamic <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static dynamic AsExpandoObject(this object source)
        {
            var expando = new ExpandoObject();

            foreach (var property in source.GetType().GetProperties())
                ((IDictionary<string,object>)expando)[property.Name] = property.GetValue(source, null);

            return expando;
        }
    }
}
