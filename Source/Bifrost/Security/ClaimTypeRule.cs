/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityRule"/> for roles
    /// </summary>
    public class ClaimTypeRule : ISecurityRule
    {
        /// <summary>
        /// The format string for describing the rule
        /// </summary>
        public const string DescriptionFormat = @"RequiredClaim_{{{0}}}";

        IUserSecurityActor _userToAuthorize;

        /// <summary>
        /// Initializes a new instance of <see cref="RoleRule"/>
        /// </summary>
        /// <param name="userToAuthorize">The <see cref="UserSecurityActor" /> to check the role against.</param>
        /// <param name="claimType">The claim type to check for</param>
        public ClaimTypeRule(IUserSecurityActor userToAuthorize, string claimType)
        {
            _userToAuthorize = userToAuthorize;
            ClaimType = claimType;
        }

        /// <summary>
        /// Gets the claimtype for the rule
        /// </summary>
        public string ClaimType { get; private set; }

        /// <inheritdoc/>
        public bool IsAuthorized(object securable)
        {
            return string.IsNullOrWhiteSpace(ClaimType) || _userToAuthorize.HasClaimType(ClaimType);
        }

        /// <inheritdoc/>
        public string Description
        {
            get { return string.Format(DescriptionFormat, ClaimType); }
        }

    }
}
