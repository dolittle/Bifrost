/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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