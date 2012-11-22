using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Execution;
using Bifrost.Entities;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Base class for configuration elements that require specifc storage
    /// </summary>
    public class ConfigurationStorageElement : IConfigurationElement, IHaveStorage
    {
        /// <summary>
        /// The specific EntityContextConfiguration type that will be bound against
        /// </summary>
        public virtual  IEntityContextConfiguration EntityContextConfiguration { get; set; }

        /// <summary>
        /// Base method that initializes the connection on the entity context connection
        /// </summary>
        public virtual void Initialize(IContainer container)
        {
            if (EntityContextConfiguration != null)
            {
                EntityContextConfiguration.Connection.Initialize(container);
            }
        }
    }
}
