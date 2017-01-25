/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies" /> that provides assemblies from the current <see cref="AppDomain" />.
    /// </summary>
    public class AppDomainAssemblyProvider : ICanProvideAssemblies
    {
        private readonly IDictionary<AssemblyInfo, Assembly> _loadedAssemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="AppDomainAssemblyProvider" />.
        /// </summary>
        public AppDomainAssemblyProvider()
        {
            AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoaded;
            _loadedAssemblies = BuildAssemblyMap();
        }

        private void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            var assembly = args.LoadedAssembly;
            if (!assembly.IsDynamic)
            {
                _loadedAssemblies[AssemblyInfoFromAssembly(assembly)] = assembly;
                AssemblyAdded(assembly);
            }
        }

        private static IDictionary<AssemblyInfo, Assembly> BuildAssemblyMap()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .ToDictionary(AssemblyInfoFromAssembly, a => a);
        }

        private static AssemblyInfo AssemblyInfoFromAssembly(Assembly assembly)
        {
            return new AssemblyInfo(assembly.GetName().Name, assembly.Location);
        }

#pragma warning disable 1591 // Xml Comments
        public event AssemblyAdded AssemblyAdded = a => { };

        public IEnumerable<AssemblyInfo> AvailableAssemblies => _loadedAssemblies.Keys;

        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            Assembly assembly;
            _loadedAssemblies.TryGetValue(assemblyInfo, out assembly);
            return assembly;
        }
#pragma warning restore 1591 // Xml Comments
    }
}