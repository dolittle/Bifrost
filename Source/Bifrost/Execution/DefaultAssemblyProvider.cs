﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Logging;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that provides assemblies from the current context
    /// </summary>
    public class DefaultAssemblyProvider : ICanProvideAssemblies
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultAssemblyProvider"/>
        /// </summary>
        /// <param name="logger">Logger for logging</param>
        public DefaultAssemblyProvider(ILogger logger)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var dependencyModel = DependencyContext.Load(entryAssembly);
            var assemblyNames = dependencyModel.GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier());
            AvailableAssemblies = assemblyNames.Select(asm => new AssemblyInfo(asm.Name, string.Empty));
            
            foreach (var assembly in AvailableAssemblies) logger.Information($"Providing '{assembly.Name}'");
        }

        /// <inheritdoc/>
        public IEnumerable<AssemblyInfo> AvailableAssemblies { get; private set; }
        
        /// <inheritdoc/>
        public event AssemblyAdded AssemblyAdded = (asm) => {};

        /// <inheritdoc/>
        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            return Assembly.Load(new AssemblyName(assemblyInfo.Name));
        }
    }
}