/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Tenancy;

namespace Bifrost.Security
{
    /// <summary>
    /// Extensions for a <see cref="ISecurityContext"/>
    /// </summary>
    public static class SecurityContextExtensions 
    {
        /// <summary>
        /// Add a tenant condition for a <see cref="ISecurityContext"/> 
        /// </summary>
        /// <param name="context"><see cref="ISecurityContext"/> to add for</param>
        /// <param name="tenant">The <see cref="Tentant"/> criteria that must be met</param>
        /// <returns>The <see cref="ISecurityContext"/> to continue the chain</returns>
        public static ISecurityContext TenantIs(this ISecurityContext context, Tenant tenant)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ISecurityContext ExecutionContextValue(this ISecurityContext context, Expression<Func<ExecutionContext, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
