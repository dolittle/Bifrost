/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityActor"/> for a user.
    /// </summary>
    public interface IUserSecurityActor
    {
        /// <summary>
        /// Checks whether the Current user has the requested role.
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True is the user has the role, False otherwise</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Checks wether or not the Current user has the requested claim type
        /// </summary>
        /// <param name="claimType">ClaimType to check for</param>
        /// <returns>True if the user has the claimtype, false if not </returns>
        bool HasClaimType(string claimType);

        /// <summary>
        /// Checks wether or not the Current user has the requested claim type with a specific value
        /// </summary>
        /// <param name="claimType">ClaimType to check for</param>
        /// <param name="value">Value to check for</param>
        /// <returns>True if the user has the claimtype, false if not </returns>
        bool HasClaimTypeWithValue(string claimType, string value);
    }
}