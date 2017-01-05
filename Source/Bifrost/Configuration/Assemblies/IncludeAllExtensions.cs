/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Extensions;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Extensions for <see cref="IncludeAll"/>
    /// </summary>
    public static class IncludeAllExtensions
    {
        /// <summary>
        /// Include all except for assemblies that has a name starting with the given name
        /// </summary>
        /// <param name="includeAll"><see cref="IncludeAll">Configuration object</see></param>
        /// <param name="assemblyNames">Names of assemblies to exclude</param>
        /// <returns>Chain of <see cref="IncludeAll">configuration object</see></returns>
        public static IncludeAll ExceptAssembliesStartingWith(this IncludeAll includeAll, params string[] assemblyNames)
        {
            var specification = includeAll.Specification;
            assemblyNames.ForEach(assemblyName => specification = specification.And(new ExceptAssembliesStartingWith(assemblyName)));
            includeAll.Specification = specification;
            return includeAll;
        }
    }
}
