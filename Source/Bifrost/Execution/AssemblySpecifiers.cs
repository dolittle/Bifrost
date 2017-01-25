/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
        readonly ITypeFinder _typeFinder;
        readonly IContractToImplementorsMap _contractToImplementorsMap;
        readonly IAssembliesConfiguration _assembliesConfiguration;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblySpecifiers"/>
        /// </summary>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding types</param>
        /// <param name="assembliesConfiguration"><see cref="IAssembliesConfiguration"/> used for building the rules for assemblies</param>
        public AssemblySpecifiers(
            IContractToImplementorsMap contractToImplementorsMap,
            ITypeFinder typeFinder,
            IAssembliesConfiguration assembliesConfiguration)
        {
            _typeFinder = typeFinder;
            _assembliesConfiguration = assembliesConfiguration;
            _contractToImplementorsMap = contractToImplementorsMap;
        }

#pragma warning disable 1591 // Xml Comments
        public void SpecifyUsingSpecifiersFrom(Assembly assembly)
        {
            _typeFinder
                .FindMultiple<ICanSpecifyAssemblies>(_contractToImplementorsMap)
                .Where(t => t.GetTypeInfo().Assembly.FullName == assembly.FullName)
                .Where(type => type.HasDefaultConstructor())
                .ForEach(type =>
                {
                    var specifier = Activator.CreateInstance(type) as ICanSpecifyAssemblies;
                    specifier.Specify(_assembliesConfiguration);
                });
        }
#pragma warning restore 1591 // Xml Comments
    }
}
