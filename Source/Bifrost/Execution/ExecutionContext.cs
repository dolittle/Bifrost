/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Principal;
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
        /// <param name="principal"><see cref="IPrincipal"/> to populate with</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> for the <see cref="ExecutionContext"/></param>
        /// <param name="detailsPopulator">Callback that gets called for populating the details of the <see cref="ExecutionContext"/></param>
        /// <param name="system">Name of the system that is running</param>
        public ExecutionContext(IPrincipal principal, CultureInfo cultureInfo, ExecutionContextPopulator detailsPopulator, string system)
        {
            Principal = principal;
            Culture = cultureInfo;
            System = system;
            Details = new WriteOnceExpandoObject(d => detailsPopulator(this,d));
        }

#pragma warning disable 1591 // Xml Comments
        public IPrincipal Principal { get; private set; }
        public CultureInfo Culture { get; private set; }
        public string System { get; private set; }
        public ITenant Tenant { get; set; }
        public dynamic Details { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
