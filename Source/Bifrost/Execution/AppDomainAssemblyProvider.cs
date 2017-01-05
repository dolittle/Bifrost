/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => 
                    !assembly.IsDynamic && 
                    assembly.GetName().Name == assemblyInfo.Name).SingleOrDefault();
        }
#pragma warning restore 1591 // Xml Comments

        void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {

            AssemblyAdded(args.LoadedAssembly);
        }
    }
}
