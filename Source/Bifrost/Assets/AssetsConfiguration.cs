using System.Collections.Generic;

namespace Bifrost.Assets
{
    /// <summary>
    /// Represents the configuration for Assets
    /// </summary>
    public class AssetsConfiguration
    {
        /// <summary>
        /// List of paths to be excluded from assets evaluation
        /// </summary>
        public IList<string> PathsToExclude { get; set; }
    }
}
