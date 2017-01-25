using Bifrost.Extensions;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Extensions for <see cref="IncludeNone"/>.
    /// </summary>
    public static class IncludeNoneExtensions
    {
        /// <summary>
        /// Include all assemblies that has a name starting with the given name
        /// </summary>
        /// <param name="includeNone"><see cref="IncludeNone">Configuration object</see></param>
        /// <param name="assemblyNames">Names of assemblies to exclude</param>
        /// <returns>Chain of <see cref="IncludeAll">configuration object</see></returns>
        public static IncludeNone ExceptAssembliesStartingWith(this IncludeNone includeNone, params string[] assemblyNames)
        {
            var specification = includeNone.Specification;
            assemblyNames.ForEach(assemblyName => specification = specification.Or(new AssembliesStartingWith(assemblyName)));
            includeNone.Specification = specification;
            return includeNone;
        }
    }
}
