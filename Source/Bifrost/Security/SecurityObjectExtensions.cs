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

namespace Bifrost.Security
{
    /// <summary>
    /// Provides a fluent interface for adding certain rules to a <see cref="ISecurityObject"/>
    /// </summary>
    public static class SecurityObjectExtensions
    {
        /// <summary>
        /// Declares that the <see cref="ISecurityObject"/> must be in a specific role
        /// </summary>
        /// <param name="securityObject"><see cref="ISecurityObject"/> to declare it for</param>
        /// <param name="role">Role</param>
        /// <returns><see cref="ISecurityObject"/> to continue the chain with</returns>
        public static ISecurityObject MustBeInRole(this ISecurityObject securityObject, string role)
        {
            return securityObject;
        }

        /// <summary>
        /// Declares that the <see cref="ISecurityObject"/> must be in set of specific roles
        /// </summary>
        /// <param name="securityObject"><see cref="ISecurityObject"/> to declare it for</param>
        /// <param name="roles">Roles to specify</param>
        /// <returns><see cref="ISecurityObject"/> to continue the chain with</returns>
        public static ISecurityObject MustBeInRoles(this ISecurityObject securityObject, params string[] roles)
        {
            return securityObject;
        }

        /// <summary>
        /// Declares that the <see cref="ISecurityObject"/> must satisfy a certain condition that is specified by a callback
        /// </summary>
        /// <param name="securityObject"><see cref="ISecurityObject"/> to declare it for</param>
        /// <param name="func">The callback that gets called</param>
        /// <returns><see cref="ISecurityObject"/> to continue the chain with</returns>
        public static ISecurityObject MustSatisfy(this ISecurityObject securityObject, Func<ISecurityObject, bool> func)
        {
            return securityObject;
        }
    }
}
