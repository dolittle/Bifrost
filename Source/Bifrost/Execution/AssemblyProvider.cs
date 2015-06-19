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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Bifrost.Collections;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyProvider"/>
    /// </summary>
    [Singleton]
    public class AssemblyProvider : IAssemblyProvider
    {
        static object _lockObject = new object();

        AssemblyComparer comparer = new AssemblyComparer();

        IEnumerable<ICanProvideAssemblies> _assemblyProviders;
        IAssemblyFilters _assemblyFilters;
        IAssemblyUtility _assemblyUtility;
        IAssemblySpecifiers _assemblySpecifiers;
        IContractToImplementorsMap _contractToImplementorsMap;
        ObservableCollection<_Assembly> _assemblies = new ObservableCollection<_Assembly>();

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyProvider"/>
        /// </summary>
        /// <param name="assemblyProviders"><see cref="IEnumerable{ICanProvideAssemblies}">Providers</see> to provide assemblies</param>
        /// <param name="assemblyFilters"><see cref="IAssemblyFilters"/> to use for filtering assemblies through</param>
        /// <param name="assemblyUtility">An <see cref="IAssemblyUtility"/></param>
        /// <param name="assemblySpecifiers"><see cref="IAssemblySpecifiers"/> used for specifying what assemblies to include or not</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        public AssemblyProvider(
            IEnumerable<ICanProvideAssemblies> assemblyProviders,
            IAssemblyFilters assemblyFilters, 
            IAssemblyUtility assemblyUtility,
            IAssemblySpecifiers assemblySpecifiers,
            IContractToImplementorsMap contractToImplementorsMap)
        {
            _assemblyProviders = assemblyProviders;
            _assemblyFilters = assemblyFilters;
            _assemblyUtility = assemblyUtility;
            _assemblySpecifiers = assemblySpecifiers;
            _contractToImplementorsMap = contractToImplementorsMap;

            HookUpAssemblyAddedForProviders();
            Populate();
        }

#pragma warning disable 1591 // Xml Comments
        public IObservableCollection<_Assembly> GetAll()
        {
            return _assemblies;
        }
#pragma warning restore 1591 // Xml Comments
        void HookUpAssemblyAddedForProviders()
        {
            foreach (var provider in _assemblyProviders)
                provider.AssemblyAdded += AssemblyLoaded;
        }

        void AssemblyLoaded(Assembly assembly)
        {
            if (_assemblyUtility.IsAssemblyDynamic(assembly)) return;

            var assemblyFile = new FileInfo(assembly.Location);
            if (!_assemblyFilters.ShouldInclude(assemblyFile.Name)) return;
            AddAssembly(assembly);
        }

        void Populate()
        {
            foreach (var provider in _assemblyProviders)
            {
                var assembliesToInclude = provider.AvailableAssemblies.Where(
                    a => 
                        _assemblyFilters.ShouldInclude(a.FileName) && 
                        _assemblyUtility.IsAssembly(a)
                    );
                assembliesToInclude.Select(provider.Get).ForEach(AddAssembly);
            }
        }

        void SpecifyRules(_Assembly assembly)
        {
            _assemblySpecifiers.SpecifyUsingSpecifiersFrom(assembly);
        }

        void ReapplyFilter()
        {
            var assembliesToRemove = _assemblies.Where(a => !_assemblyFilters.ShouldInclude(a.GetName().Name)).ToArray();
            assembliesToRemove.ForEach((a) =>_assemblies.Remove(a));
        }

        void AddAssembly(_Assembly assembly)
        {
            lock (_lockObject)
            {
                if (!_assemblies.Contains(assembly, comparer) &&
                    !_assemblyUtility.IsAssemblyDynamic(assembly))
                {
                    _assemblies.Add(assembly);

                    if (assembly.FullName.Contains("Web"))
                    {
                        var i = 0;
                        i++;
                    }
                    _contractToImplementorsMap.Feed(assembly.GetTypes());
                    SpecifyRules(assembly);
                    ReapplyFilter();
                }
            }
        }

        bool Matches(AssemblyName a, AssemblyName b)
        {
            return a.ToString() == b.ToString();
        }
    }
}
