/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

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
