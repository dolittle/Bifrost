using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Interface for all configuration elements
    /// </summary>
    public interface IConfigurationElement
    {
        /// <summary>
        /// Initialization of the deriving ConfigurationElement instance
        /// </summary>
        void Initialize(IContainer container);
    }
}
