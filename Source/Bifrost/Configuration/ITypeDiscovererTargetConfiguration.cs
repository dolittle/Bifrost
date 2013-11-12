namespace Bifrost.Configuration
{
    /// <summary>
    /// The target configuration for types
    /// </summary>
    public interface ITypeDiscovererTargetConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Add approved assembly name
        /// </summary>
        /// <param name="name">Full/partial assembly name</param>
        void AddAssembly(string name);
    }
}
