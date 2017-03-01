/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        ObservableCollection<Assembly> _assemblies = new ObservableCollection<Assembly>();

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
        public IObservableCollection<Assembly> GetAll()
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
            if (_assemblyUtility.IsAssemblyDynamic(assembly))
            {
                return;
            }

            var newSpecifiers = _assemblySpecifiers.SpecifyUsingSpecifiersFrom(assembly);
            AddAssembly(assembly);
            if (newSpecifiers)
            {
                RecalculateAssemblies();
            }
        }

        void Populate()
        {
            var assemblies = AvailableAssemblies();
            assemblies.ForEach(a => _assemblySpecifiers.SpecifyUsingSpecifiersFrom(a));
            assemblies.ForEach(AddAssembly);
        }

        IList<Assembly> AvailableAssemblies()
        {
            return _assemblyProviders
                .SelectMany(p => p.AvailableAssemblies, (provider, assemblyInfo) => new {provider, assemblyInfo})
                .GroupBy(pair => pair.assemblyInfo.Name)
                .Select(group => group.First())
                .Where(pair => _assemblyUtility.IsAssembly(pair.assemblyInfo))
                .Select(pair => pair.provider.Get(pair.assemblyInfo))
                .Where(assembly => !assembly.IsDynamic)
                .ToList();
        }

        bool ShouldInclude(Assembly assembly)
        {
            return _assemblyFilters.ShouldInclude(new FileInfo(assembly.Location).Name);
        }

        void AddAssembly(Assembly assembly)
        {
            lock (_lockObject)
            {
                if (!_assemblies.Contains(assembly, comparer) && ShouldInclude(assembly))
                {
                    _assemblies.Add(assembly);
                    _contractToImplementorsMap.Feed(assembly.GetTypes());
                }
            }
        }

        void RecalculateAssemblies()
        {
            var assemblies = AvailableAssemblies();
            assemblies.ForEach(AddAssembly);
        }
    }
}
