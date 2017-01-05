/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an <see cref="IViewsConfiguration"/>
    /// </summary>
    public class ViewsConfiguration : ConfigurationStorageElement, IViewsConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public override void Initialize(IContainer container)
        {
            base.Initialize(container);
        }
#pragma warning restore // Xml Comments
    }
}
