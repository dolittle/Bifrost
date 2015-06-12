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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that provides assemblies from the current <see cref="_AppDomain"/>
    /// </summary>
    public class AppDomainAssemblyProvider : ICanProvideAssemblies
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AppDomainAssemblyProvider"/>
        /// </summary>
        public AppDomainAssemblyProvider()
        {
            AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoaded;

        }

#pragma warning disable 1591 // Xml Comments
        public event AssemblyAdded AssemblyAdded = (a) => { };

        public IEnumerable<AssemblyInfo> AvailableAssemblies
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !assembly.IsDynamic)
                    .Select(assembly =>
                    {
                        var name = assembly.GetName();
                        var assemblyInfo = new AssemblyInfo(name.Name, assembly.Location);
                        return assemblyInfo;
                    });
            }
        }

        public _Assembly Get(AssemblyInfo assemblyInfo)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => 
                    !assembly.IsDynamic && 
                    assembly.Location == assemblyInfo.Path).SingleOrDefault();
        }
#pragma warning restore 1591 // Xml Comments

        void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            AssemblyAdded(args.LoadedAssembly);
        }
    }
}
