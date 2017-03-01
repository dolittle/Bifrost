using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents the <see cref="IAssembliesConfiguration"/> for building the <see cref="IncludeNoneRule"/>.
    /// </summary>
    public class IncludeNone : IAssembliesConfiguration
    {
        /// <summary>
        /// Initializes an instance of <see cref="IncludeNone"/>.
        /// </summary>
        public IncludeNone()
        {
            Specification = new IncludeNoneRule();
        }

        /// <summary>
        /// Gets the <see cref="IncludeNoneRule"/>.
        /// </summary>
        public Specification<string> Specification { get; set; }
    }
}
