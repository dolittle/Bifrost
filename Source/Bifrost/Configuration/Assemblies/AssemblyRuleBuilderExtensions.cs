/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Provides extensions for <see cref="IAssemblyRuleBuilder"/>
    /// </summary>
    public static class AssemblyRuleBuilderExtensions
    {

        /// <summary>
        /// Excludes specified assemblies
        /// </summary>
        /// <param name="assemblyBuilder"><see cref="IAssemblyBuilder"/> to build upon</param>
        /// <param name="names">Names that assemblies should not be starting with</param>
        /// <returns>Chained <see cref="IAssemblyBuilder"/></returns>
        public static IAssemblyRuleBuilder ExcludeAssembliesStartingWith(this IAssemblyRuleBuilder assemblyBuilder, params string[] names)
        {
            assemblyBuilder.Specification = assemblyBuilder.Specification.And(new ExceptAssembliesStartingWith(names));
            return assemblyBuilder;
        }
    }
}
