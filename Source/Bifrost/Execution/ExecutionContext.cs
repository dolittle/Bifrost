/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using Bifrost.Applications;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContext"/>
    /// </summary>
    [IgnoreDefaultConvention]
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContext"/>
        /// </summary>
        /// <param name="principal"><see cref="ClaimsPrincipal"/> to populate with</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> for the <see cref="ExecutionContext"/></param>
        /// <param name="detailsPopulator">Callback that gets called for populating the details of the <see cref="ExecutionContext"/></param>
        /// <param name="application"><see cref="IApplication"/> that is currently executing</param>
        /// <param name="tenant"><see cref="ITenant"/> that is currently part of the <see cref="IExecutionContext"/></param>
        public ExecutionContext(ClaimsPrincipal principal, CultureInfo cultureInfo, ExecutionContextPopulator detailsPopulator, IApplication application, ITenant tenant)
        {
            Principal = principal;
            Culture = cultureInfo;
            Application = application;
            Tenant = tenant;
            Details = new WriteOnceExpandoObject(d => detailsPopulator(this,d));
        }

        /// <inheritdoc/>
        public ClaimsPrincipal Principal { get; }

        /// <inheritdoc/>
        public CultureInfo Culture { get; }

        /// <inheritdoc/>
        public IApplication Application { get; }

        /// <inheritdoc/>
        public ITenant Tenant { get; }

        /// <inheritdoc/>
        public dynamic Details { get; }
    }
}
