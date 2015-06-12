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
        /// <param name="assemblyInfo"><see cref="AssemblyInfo"/> to check</param>
        /// <returns>True if the file is an <see cref="_Assembly"/>, false if not</returns>
        bool IsAssembly(AssemblyInfo assemblyInfo);

        /// <summary>
        /// Check if an <see cref="_Assembly"/> is dynamic
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <remarks>
        /// The need for this is questionable - the interface <see cref="_Assembly"/> does not have the IsDynamic
        /// property as the implementation <see cref="Assembly"/> has. This might go away as there has been
        /// a realization that <see cref="_Assembly"/> might not be needed, it was introduced to do testing
        /// easier. Turns out however that the implementation <see cref="Assembly"/> has pretty much everything
        /// virtual.
        /// </remarks>
        bool IsAssemblyDynamic(_Assembly assembly);
    }
}
