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

namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines something can deal with creating proxy types
    /// </summary>
    public interface IProxying
    {
        /// <summary>
        /// Build an interface type that contains the properties from a specific other type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <returns>A new <see cref="Type"/></returns>
        Type BuildInterfaceWithPropertiesFrom(Type type);

        /// <summary>
        /// Build a class type that contains the properties from a specific other type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get properties from</param>
        /// <returns>A new <see cref="Type"/></returns>
        Type BuildClassWithPropertiesFrom(Type type);
    }
}
