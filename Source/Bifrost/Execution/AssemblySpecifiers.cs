/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
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
        static readonly object LockObject = new object();

        readonly IAssembliesConfiguration _assembliesConfiguration;
        readonly ISet<string> _specifiedAssemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblySpecifiers"/>
        /// </summary>
        public AssemblySpecifiers(IAssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
            _specifiedAssemblies = new HashSet<string>();
        }

#pragma warning disable 1591 // Xml Comments
        public bool SpecifyUsingSpecifiersFrom(Assembly assembly)
        {
            lock (LockObject)
            {
                if (_specifiedAssemblies.Contains(assembly.FullName))
                {
                    return false;
                }

                _specifiedAssemblies.Add(assembly.FullName);

                var specified = false;
                if (MayReferenceICanSpecifyAssemblies(assembly))
                {
                    assembly
                        .GetTypes()
                        .Where(t => t.GetTypeInfo().GetInterfaces().Contains(typeof(ICanSpecifyAssemblies)))
                        .Where(t => t.HasDefaultConstructor())
                        .ForEach(t =>
                        {
                            var specifier = Activator.CreateInstance(t) as ICanSpecifyAssemblies;
                            specifier.Specify(_assembliesConfiguration);
                            specified = true;
                        });
                }

                return specified;
            }
        }

        static bool MayReferenceICanSpecifyAssemblies(Assembly assembly)
        {
            return
                assembly.FullName.Contains("Bifrost") ||
                assembly.GetReferencedAssemblies().Any(a => a.FullName.Contains("Bifrost"));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
