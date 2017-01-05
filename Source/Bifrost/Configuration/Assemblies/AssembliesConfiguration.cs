/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents the configuration for Assemblies
    /// </summary>
    public class AssembliesConfiguration
    {
        IAssemblyRuleBuilder _assemblyRuleBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="AssembliesConfiguration"/>
        /// </summary>
        /// <param name="assemblyRuleBuilder"><see cref="IAssemblyRuleBuilder"/> that builds the rules</param>
        public AssembliesConfiguration(IAssemblyRuleBuilder assemblyRuleBuilder)
        {
            _assemblyRuleBuilder = assemblyRuleBuilder;
        }

        /// <summary>
        /// Gets the specification used to specifying which assemblies to include
        /// </summary>
        public Specification<string> Specification { get { return _assemblyRuleBuilder.Specification; } }
    }
}
