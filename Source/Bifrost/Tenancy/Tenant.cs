/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents a <see cref="ITenant"/> in the system
    /// </summary>
    public class Tenant : ITenant
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Tenant"/>
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> of the tenant</param>
        /// <param name="details">Dynamic object holding details</param>
        public Tenant(TenantId tenantId, dynamic details)
        {
            TenantId = tenantId;
            Details = details;
        }

        /// <inheritdoc/>
        public TenantId TenantId { get; }

        /// <inheritdoc/>
        public dynamic Details { get; }

    }
}