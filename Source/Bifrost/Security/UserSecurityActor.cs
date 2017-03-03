/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a concrete <see cref="SecurityActor"/> for a user
    /// </summary>
    public class UserSecurityActor : SecurityActor, IUserSecurityActor
    {
        readonly ICanResolvePrincipal _resolvePrincipal;

        /// <summary>
        /// Description of the <see cref="UserSecurityActor"/>
        /// </summary>
        public const string USER = "User";

        /// <summary>
        /// Instantiates an instance of <see cref="UserSecurityActor"/>
        /// </summary>
        public UserSecurityActor(ICanResolvePrincipal resolvePrincipal) : base(USER)
        {
            _resolvePrincipal = resolvePrincipal;
        }

        /// <summary>
        /// Checks whether the Current user has the requested role.
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True is the user has the role, False otherwise</returns>
        public virtual bool IsInRole(string role)
        {
            return _resolvePrincipal.Resolve().IsInRole(role);
        }
    }
}
