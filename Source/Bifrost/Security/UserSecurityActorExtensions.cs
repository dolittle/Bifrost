/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Extensions for <see cref="UserSecurityActor"/>
    /// </summary>
    public static class UserSecurityActorExtensions
    {
        /// <summary>
        /// Declares that the <see cref="UserSecurityActor"/> must be in a specific role
        /// </summary>
        /// <param name="securityActor"><see cref="UserSecurityActor"/> to declare it for</param>
        /// <param name="role">Role</param>
        /// <returns><see cref="UserSecurityActor"/> to continue the chain with</returns>
        public static UserSecurityActor MustBeInRole(this UserSecurityActor securityActor, string role)
        {
            securityActor.AddRule(new RoleRule(securityActor,role));
            return securityActor;
        }

        /// <summary>
        /// Declares that the <see cref="UserSecurityActor"/> must be in set of specific roles
        /// </summary>
        /// <param name="securityActor"><see cref="UserSecurityActor"/> to declare it for</param>
        /// <param name="roles">Roles to specify</param>
        /// <returns><see cref="UserSecurityActor"/> to continue the chain with</returns>
        public static UserSecurityActor MustBeInRoles(this UserSecurityActor securityActor, params string[] roles)
        {
            foreach (var role in roles) securityActor.AddRule(new RoleRule(securityActor,role));

            return securityActor;
        }


        /// <summary>
        /// Declares that the <see cref="UserSecurityActor"/> must have a certain claim type
        /// </summary>
        /// <param name="securityActor"><see cref="UserSecurityActor"/> to declare it for</param>
        /// <param name="claimType">Claim type that is required</param>
        /// <returns><see cref="UserSecurityActor"/> to continue the chain with</returns>
        public static UserSecurityActor MustHaveClaimType(this UserSecurityActor securityActor, string claimType)
        {
            securityActor.AddRule(new ClaimTypeRule(securityActor, claimType));
            return securityActor;
        }

        /// <summary>
        /// Declares that the <see cref="UserSecurityActor"/> must have specific claim types
        /// </summary>
        /// <param name="securityActor"><see cref="UserSecurityActor"/> to declare it for</param>
        /// <param name="claimTypes">Claim types that are required</param>
        /// <returns><see cref="UserSecurityActor"/> to continue the chain with</returns>
        public static UserSecurityActor MustHaveClaimTypes(this UserSecurityActor securityActor, params string[] claimTypes)
        {
            foreach (var claimType in claimTypes) securityActor.AddRule(new ClaimTypeRule(securityActor, claimType));

            return securityActor;
        }

        /// <summary>
        /// Declares that the <see cref="UserSecurityActor"/> must have specific claim types
        /// </summary>
        /// <param name="securityActor"><see cref="UserSecurityActor"/> to declare it for</param>
        /// <param name="claimType">Claim type that is required</param>
        /// <param name="value">Value of the claim that is required</param>
        /// <returns><see cref="UserSecurityActor"/> to continue the chain with</returns>
        public static UserSecurityActor MustHaveClaimTypeWithValue(this UserSecurityActor securityActor, string claimType, string value)
        {
            securityActor.AddRule(new ClaimTypeAndValueRule(securityActor, claimType, value));
            return securityActor;
        }
    }
}
