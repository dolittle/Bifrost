#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
