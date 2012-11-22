using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines configuration for default storage
    /// </summary>
    public interface IDefaultStorageConfiguration : IHaveStorage, IConfigurationElement
    {
    }
}
