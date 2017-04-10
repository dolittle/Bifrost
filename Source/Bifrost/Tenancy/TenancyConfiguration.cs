/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
 
 namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ITenancyConfiguration"/>
    /// </summary>
    public class TenancyConfiguration : ITenancyConfiguration
    {
        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            var typeDiscoverer = container.Get<ITypeDiscoverer>();

        }
    }
}
