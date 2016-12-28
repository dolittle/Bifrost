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
using System.Linq;
using System.Reflection;
using Bifrost.Configuration.Assemblies;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblySpecifiers"/>
    /// </summary>
    public class AssemblySpecifiers : IAssemblySpecifiers
    {
        ITypeFinder _typeFinder;
        IContractToImplementorsMap _contractToImplementorsMap;
        IAssemblyRuleBuilder _assemblyRuleBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblySpecifiers"/>
        /// </summary>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding types</param>
        /// <param name="assemblyRuleBuilder"><see cref="IAssemblyRuleBuilder"/> used for building the rules for assemblies</param>
        public AssemblySpecifiers(IContractToImplementorsMap contractToImplementorsMap, ITypeFinder typeFinder, IAssemblyRuleBuilder assemblyRuleBuilder)
        {
            _typeFinder = typeFinder;
            _assemblyRuleBuilder = assemblyRuleBuilder;
            _contractToImplementorsMap = contractToImplementorsMap;
        }

#pragma warning disable 1591 // Xml Comments
        public void SpecifyUsingSpecifiersFrom(Assembly assembly)
        {
            var assemblySpecifiers = _typeFinder.FindMultiple<ICanSpecifyAssemblies>(_contractToImplementorsMap);
            assemblySpecifiers.Where(type => type.HasDefaultConstructor()).ForEach(type =>
            {
                var specifier = Activator.CreateInstance(type) as ICanSpecifyAssemblies;
                specifier.Specify(_assemblyRuleBuilder);
            });
        }
#pragma warning restore 1591 // Xml Comments
    }
}
