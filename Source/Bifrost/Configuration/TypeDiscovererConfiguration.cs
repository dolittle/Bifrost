using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeDiscovererConfiguration"/>
    /// </summary>
    public class TypeDiscovererConfiguration : ITypeDiscovererConfiguration
    {
        /// <summary>
        /// The target configuration
        /// </summary>
        public ITypeDiscovererTargetConfiguration Target { get; set; }

        /// <summary>
        /// Initialize the configuration
        /// </summary>
        /// <param name="container"></param>
        public void Initialize(IContainer container)
        {
            if (Target != null)
                Target.Initialize(container);
        }

    }
}
