namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the configuration for type discovering
    /// </summary>
    public interface ITypeDiscovererConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets or sets the <see cref="ITypeDiscovererTargetConfiguration"/> to use for configuring the type discoverer
        /// </summary>
        ITypeDiscovererTargetConfiguration Target { get; set; }
        
    }
}