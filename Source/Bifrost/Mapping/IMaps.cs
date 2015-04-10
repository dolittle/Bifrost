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

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a system for knowing about maps
    /// </summary>
    public interface IMaps
    {
        /// <summary>
        /// Check if there is a map for the given combination of source and target
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see></param>
        /// <param name="target"><see cref="Type">Target type</see></param>
        /// <returns>true if there is a map, false if not</returns>
        bool HasFor(Type source, Type target);

        /// <summary>
        /// Get a map for specific source and target types
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see></param>
        /// <param name="target"><see cref="Type">Target type</see></param>
        /// <returns><see cref="IMap"/> for the combination</returns>
        IMap GetFor(Type source, Type target);
    }
}
