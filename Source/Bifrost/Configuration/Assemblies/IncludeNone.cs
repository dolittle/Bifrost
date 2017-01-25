using Bifrost.Specifications;

namespace Bifrost.Configuration.Assemblies
{
    /// <summary>
    /// Represents the <see cref="IAssemblyRuleBuilder">builder</see> for building the <see cref="IncludeNoneRule"/> and
    /// possible exceptions.
    /// </summary>
    public class IncludeNone : IAssemblyRuleBuilder
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
