/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Security.Claims;
using Bifrost.Execution;

namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanResolveTenantId"/>
    /// </summary>
    public class DefaultTenantIdResolver : ICanResolveTenantId
    {
        const string _tenantId = "http://schemas.microsoft.com/identity/claims/tenantid";
        const string _identityProvider = "http://schemas.microsoft.com/identity/claims/identityprovider";

        IExecutionContext _executionContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionContext"></param>
        public DefaultTenantIdResolver(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        /// <inheritdoc/>
        public TenantId Resolve()
        {
            ClaimsPrincipal principal = _executionContext.Principal;
            var claim = principal.Claims.FirstOrDefault(c => c.Type == _tenantId);
            if (claim == null) claim = principal.Claims.FirstOrDefault(c => c.Type == _identityProvider);
            if (claim == null) claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            var tenantId = (claim != null) ? claim.Value : "unknown";
            return tenantId;
        }
    }
}
