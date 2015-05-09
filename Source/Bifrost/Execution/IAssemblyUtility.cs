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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a utility to work with <see cref="_Assembly"/>
    /// </summary>
    public interface IAssemblyUtility
    {
        /// <summary>
        /// Check if file is an actual .NET assembly or not
        /// </summary>
        /// <param name="fileInfo"><see cref="FileInfo"/> to check</param>
        /// <returns>True if the file is an <see cref="_Assembly"/>, false if not</returns>
        bool IsAssembly(FileInfo fileInfo);

        /// <summary>
        /// Get <see cref="AssemblyName"/> for a <see cref="FileInfo">file</see>
        /// </summary>
        /// <param name="fileInfo"><see cref="FileInfo"/> representing the file</param>
        /// <returns><see cref="AssemblyName"/> for the assembly</returns>
        AssemblyName GetAssemblyNameForFile(FileInfo fileInfo);

        /// <summary>
        /// Load an <see cref="_Assembly"/> based on its <see cref="AssemblyName"/>
        /// </summary>
        /// <param name="assemblyName"><see cref="AssemblyName"/> of the <see cref="_Assembly"/> to load</param>
        /// <returns><see cref="_Assembly"/> that got loaded</returns>
        _Assembly Load(AssemblyName assemblyName);
    }
}
