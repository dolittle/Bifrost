using System.Reflection;
using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents a <see cref="Specification{T}">rule</see> specific to <see cref="Assembly">assemblies</see>
    /// that matches no assemblies.
    /// </summary>
    public class IncludeNoneRule : Specification<string>
    {
        /// <summary>
        /// Initializes an instance of <see cref="IncludeNoneRule"/>.
        /// </summary>
        public IncludeNoneRule()
        {
            Predicate = a => false;
        }
    }
}
