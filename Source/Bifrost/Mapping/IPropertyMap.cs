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
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a map for a property on an object
    /// </summary>
    public interface IPropertyMap
    {
        /// <summary>
        /// Gets the <see cref="PropertyInfo">property</see> in which we are mapping from
        /// </summary>
        PropertyInfo From { get; }

        /// <summary>
        /// Gets or sets the <see cref="IPropertyMappingStrategy">strategy</see> to use
        /// </summary>
        IPropertyMappingStrategy Strategy { get; set; }
    }
}
