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
        _AppDomain _appDomain;
        IAssemblyFilters _assemblyFilters;
        IExecutionEnvironment _executionEnvironment;
        IAssemblyUtility _assemblyUtility;
        IAssemblySpecifiers _assemblySpecifiers;
        ObservableCollection<_Assembly> _assemblies = new ObservableCollection<_Assembly>();

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyProvider"/>
        /// </summary>
        /// <param name="appDomain">Currently running <see cref="_AppDomain"/></param>
        /// <param name="assemblyFilters"><see cref="IAssemblyFilters"/> to use for filtering assemblies through</param>
        /// <param name="executionEnvironment"><see cref="IExecutionEnvironment"/> giving us functionality needed from the currently executing environment</param>
        /// <param name="assemblyUtility">An <see cref="IAssemblyUtility"/></param>
        /// <param name="assemblySpecifiers"><see cref="IAssemblySpecifiers"/> used for specifying what assemblies to include or not</param>
        public AssemblyProvider(
            _AppDomain appDomain, 
            IAssemblyFilters assemblyFilters, 
            IExecutionEnvironment executionEnvironment,
            IAssemblyUtility assemblyUtility,
            IAssemblySpecifiers assemblySpecifiers)
        {
            _appDomain = appDomain;
            _assemblyFilters = assemblyFilters;
            _executionEnvironment = executionEnvironment;
            _assemblyUtility = assemblyUtility;
            _assemblySpecifiers = assemblySpecifiers;
            appDomain.AssemblyLoad += AssemblyLoaded;

            Populate();
        }

#pragma warning disable 1591 // Xml Comments
        public IObservableCollection<_Assembly> GetAll()
        {
            return _assemblies;
        }
#pragma warning restore 1591 // Xml Comments

        void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            if (args.LoadedAssembly.IsDynamic) return;

            var assemblyFile = new FileInfo(args.LoadedAssembly.Location);
            if (!_assemblyFilters.ShouldInclude(assemblyFile.Name)) return;
            AddAssembly(args.LoadedAssembly);
        }

        void Populate()
        {
            var files = _executionEnvironment.GetReferencedAssembliesFileInfo();

            files = files.Where(_assemblyUtility.IsAssembly);

            var currentAssemblies = new List<_Assembly>();
            currentAssemblies.AddRange(
                _appDomain.GetAssemblies()
                    .Where(a => _assemblyFilters.ShouldInclude(a.GetName().Name)
                )
            );

            currentAssemblies.ForEach(SpecifyRules);

            foreach (var file in files)
            {
                if (!_assemblyFilters.ShouldInclude(file.Name)) continue;

                var assemblyName = _assemblyUtility.GetAssemblyNameForFile(file);
                if (!currentAssemblies.Any(assembly => Matches(assemblyName, assembly.GetName())))
                    currentAssemblies.Add(_assemblyUtility.Load(assemblyName));
            }

            var assemblies = currentAssemblies.Distinct(comparer);
            assemblies.ForEach(AddAssembly);
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

        bool IsAssemblyDynamic(_Assembly assembly)
        {
            var module = assembly.GetModules().FirstOrDefault();
            if (module != null && module.GetType().Name == "InternalModuleBuilder") return true;
            return false;
        }

        

        void AddAssembly(_Assembly assembly)
        {
            lock (_lockObject)
            {
                if (!_assemblies.Contains(assembly, comparer) && !IsAssemblyDynamic(assembly) )
                {
                    _assemblies.Add(assembly);
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
