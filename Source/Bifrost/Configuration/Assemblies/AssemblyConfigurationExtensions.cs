/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Provides extensions for <see cref="IAssembliesConfiguration"/>.
    /// </summary>
    public static class AssemblyConfigurationExtensions
    {
        /// <summary>
        /// Includes specified assemblies.
        /// </summary>
        /// <param name="assembliesConfiguration"><see cref="IAssembliesConfiguration"/> to build upon.</param>
        /// <param name="names">Names that assemblies should be starting with.</param>
        /// <returns>Chained <see cref="IAssembliesConfiguration"/></returns>
        public static IAssembliesConfiguration IncludeAssembliesStartingWith(
            this IAssembliesConfiguration assembliesConfiguration, params string[] names)
        {
            assembliesConfiguration.Specification = assembliesConfiguration.Specification.Or(new AssembliesStartingWith(names));
            return assembliesConfiguration;
        }
    }
}
