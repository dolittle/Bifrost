using System.Linq;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Rule representing an addition to <see cref="IncludeNoneRule"/>,
    /// that includes assemblies in files starting with any of the given names.
    /// </summary>
    public class AssembliesStartingWith : Specification<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="AssembliesStartingWith"/>.
        /// </summary>
        /// <param name="names">List of assembly name prefixes.</param>
        public AssembliesStartingWith(params string[] names)
        {
            Predicate = a => names.Any(a.StartsWith);
        }
    }
}
