
using System;
namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the statistics specific configuration
    /// </summary>
    public interface IStatisticsConfiguration : IConfigurationElement
    {
        /// <summary>
        /// The type of store used by statistics
        /// </summary>
        Type StoreType { get; set; }
    }
}
