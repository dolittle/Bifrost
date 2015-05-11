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
using System.Runtime.InteropServices;
using Bifrost.Configuration.Assemblies;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system that knows about <see cref="ICanSpecifyAssembly"/>
    /// </summary>
    public interface IAssemblySpecifiers
    {
        /// <summary>
        /// Specifies using specifiers found in a specific <see cref="_Assembly"/>
        /// </summary>
        /// <param name="assembly"><see cref="_Assembly"/> to find specifiers from</param>
        void SpecifyUsingSpecifiersFrom(_Assembly assembly);
    }
}
