/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Entities;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IDefaultStorageConfiguration"/>
    /// </summary>
    public class DefaultStorageConfiguration : ConfigurationStorageElement, IDefaultStorageConfiguration
    {
#pragma warning disable 1591 // Xml Comments

        public override void Initialize(IContainer container)
        {
            if (EntityContextConfiguration == null)
                EntityContextConfiguration = new NullEntityContextConfiguration();

            EntityContextConfiguration.BindDefaultEntityContext(container);
            base.Initialize(container);
        }
#pragma warning restore 1591  //Xml Comments
    }
}
