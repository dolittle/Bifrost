using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (EntityContextConfiguration != null)
            {
                EntityContextConfiguration.BindDefaultEntityContext(container);
            }
            base.Initialize(container);
        }
#pragma warning restore 1591  //Xml Comments
    }
}
