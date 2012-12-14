using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Marker interface that to indicate the need of storage configuration
    /// </summary>
    public interface IHaveStorage
    {
        /// <summary>
        /// Gets or sets the entity context configuration
        /// </summary>
        IEntityContextConfiguration EntityContextConfiguration { get; set; }
    }
}
