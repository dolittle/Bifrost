/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using Bifrost.Configuration;
using Bifrost.Security;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextFactory"/>
    /// </summary>
    public class ExecutionContextFactory : IExecutionContextFactory
    {
        ICanResolvePrincipal _principalResolver;
        IExecutionContextDetailsPopulator _detailsPopulator;
        IConfigure _configure;
        ITenantManager _tenantManager;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextFactory"/>
        /// </summary>
        /// <param name="principalResolver"><see cref="ICanResolvePrincipal"/> for resolving the identity</param>
        /// <param name="detailsPopulator">A <see cref="IExecutionContextDetailsPopulator"/> to use for populating any <see cref="IExecutionContext"/> being created</param>
        /// <param name="configure">A <see cref="IConfigure"/> instance holding all configuration</param>
        /// <param name="tenantManager">A <see cref="ITenantManager"/> to get <see cref="ITenant">tenants</see> from</param>
        public ExecutionContextFactory(ICanResolvePrincipal principalResolver, IExecutionContextDetailsPopulator detailsPopulator, IConfigure configure, ITenantManager tenantManager)
        {
            _principalResolver = principalResolver;
            _detailsPopulator = detailsPopulator;
            _configure = configure;
            _tenantManager = tenantManager;
        }

#pragma warning disable 1591 // Xml Comments
        public IExecutionContext Create()
        {
            var executionContext = new ExecutionContext(
                _principalResolver.Resolve(),
                CultureInfo.CurrentCulture,
                _detailsPopulator.Populate,
                _configure.SystemName);

            executionContext.Tenant = _tenantManager.Current;

            return executionContext;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
