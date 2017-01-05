/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tenancy
{
    /// <summary>
    /// Defines a manager for <see cref="Tenant"/>
    /// </summary>
    public interface ITenantManager
    {
        /// <summary>
        /// Gets the current <see cref="ITenant"/>
        /// </summary>
        ITenant Current { get; }

    }
}
