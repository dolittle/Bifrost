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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that can provide assemblies
    /// </summary>
    public interface ICanProvideAssemblies
    {
        /// <summary>
        /// Gets triggered if an <see cref="_Assembly"/> is added dynamically
        /// </summary>
        event AssemblyAdded AssemblyAdded;

        /// <summary>
        /// Get available assemblies that can be provided
        /// </summary>
        /// <returns></returns>
        IEnumerable<AssemblyInfo> AvailableAssemblies { get; }

        /// <summary>
        /// Get a specific assembly based on its <see cref="AssemblyInfo"/>
        /// </summary>
        /// <param name="assemblyInfo"><see cref="AssemblyInfo"/> for the assembly</param>
        /// <returns>Loaded <see cref="_Assembly"/></returns>
        _Assembly Get(AssemblyInfo assemblyInfo);
    }
}
