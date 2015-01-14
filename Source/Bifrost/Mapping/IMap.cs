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

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a map that describes mapping for an object
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Gets the source type the map is for
        /// </summary>
        Type Source { get;  }

        /// <summary>
        /// Gets the target type the map is for
        /// </summary>
        Type Target { get; }

        /// <summary>
        /// Get the mapped properties
        /// </summary>
        IEnumerable<IPropertyMap> Properties { get; }
    }
}
