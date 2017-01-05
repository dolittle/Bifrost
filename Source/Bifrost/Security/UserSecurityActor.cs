/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Principal;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a concrete <see cref="SecurityActor"/> for a user
    /// </summary>
    public class UserSecurityActor : SecurityActor
    {
        /// <summary>
        /// Description of the <see cref="UserSecurityActor"/>
        /// </summary>
        public const string USER = "User";

        /// <summary>
        /// Instantiates an instance of <see cref="UserSecurityActor"/>
        /// </summary>
        public UserSecurityActor() : base(USER)
        {}

        /// <summary>
        /// Checks whether the Current user has the requested role.
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True is the user has the role, False otherwise</returns>
        public virtual bool IsInRole(string role)
        {
            return CurrentPrincipal.Get().IsInRole(role);
        }
    }
}
