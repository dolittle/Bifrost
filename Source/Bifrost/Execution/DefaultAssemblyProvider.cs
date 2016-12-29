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
using System.Reflection;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that provides assemblies from the current <see cref="_AppDomain"/>
    /// </summary>
    public class DefaultAssemblyProvider : ICanProvideAssemblies
    {
        public DefaultAssemblyProvider() 
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var dependencyModel = DependencyContext.Load(entryAssembly);
            var assemblyNames = dependencyModel.GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier());
            AvailableAssemblies = assemblyNames.Select(asm => new AssemblyInfo(asm.Name, string.Empty));
        }

        public IEnumerable<AssemblyInfo> AvailableAssemblies { get; private set; }
        
        public event AssemblyAdded AssemblyAdded;

        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            return Assembly.Load(new AssemblyName(assemblyInfo.Name));
        }
    }
}