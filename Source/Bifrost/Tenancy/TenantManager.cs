/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ITenantManager"/>
    /// </summary>
    public class TenantManager : ITenantManager
    {
        /// <summary>
        /// The key used in the <see cref="ICallContext"/> for storing the current instance
        /// </summary>
        public const string TenantKey = "Tenant";

        ICallContext _callContext;
        ITenantPopulator _tenantPopulator;
        ICanResolveTenantId _tenantIdResolver;

        /// <summary>
        /// Initializes a new instance of <see cref="TenantManager"/>
        /// </summary>
        /// <param name="callContext"><see cref="ICallContext"/> for holding current <see cref="ITenant"/></param>
        /// <param name="tenantPopulator"><see cref="ITenantPopulator"/> for populating new tenants</param>
        /// <param name="tenantIdResolver"><see cref="ICanResolveTenantId">Tenant Id resolver</see></param>
        public TenantManager(
            ICallContext callContext, 
            ITenantPopulator tenantPopulator, 
            ICanResolveTenantId tenantIdResolver)
        {
            _callContext = callContext;
            _tenantPopulator = tenantPopulator;
            _tenantIdResolver = tenantIdResolver;
        }

        /// <inheritdoc/>
        public ITenant Current
        { 
            get
            {
                ITenant current = null;

                if (_callContext.HasData(TenantKey))
                    current = _callContext.GetData<ITenant>(TenantKey);
                else
                {
                    var tenantId = _tenantIdResolver.Resolve();
                    var tenant = new Tenant(tenantId);
                    current = tenant;

                    var details = new WriteOnceExpandoObject((d) => _tenantPopulator.Populate(current, d));
                    tenant.Details = details;

                    _callContext.SetData(TenantKey, current);
                }

                return current;
            }
        }
    }
}
