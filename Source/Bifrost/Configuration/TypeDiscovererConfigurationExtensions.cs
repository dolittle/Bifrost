using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions methods to configure type discoverer
    /// </summary>
    public static class TypeDiscovererConfigurationExtensions
    {
        /// <summary>
        /// Configure type discoverer
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configureCallback"></param>
        /// <returns></returns>
        public static IConfigure Types(this ITypeDiscovererConfiguration configuration, Action<ITypeDiscovererTargetConfiguration> configureCallback)
        {
            var webConfiguration = new TypeDiscovererTargetConfiguration();
            configuration.Target = webConfiguration;
            configureCallback(webConfiguration);
            return Configure.Instance;
        }
    }
}
