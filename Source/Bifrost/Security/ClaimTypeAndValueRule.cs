/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityRule"/> for roles
    /// </summary>
    public class ClaimTypeAndValueRule : ISecurityRule
    {
        /// <summary>
        /// The format string for describing the rule
        /// </summary>
        public const string DescriptionFormat = @"RequiredClaimAndValue_{{{0}}}";

        IUserSecurityActor _userToAuthorize;

        /// <summary>
        /// Initializes a new instance of <see cref="RoleRule"/>
        /// </summary>
        /// <param name="userToAuthorize">The <see cref="UserSecurityActor" /> to check the role against.</param>
        /// <param name="claimType">The claim type to check for</param>
        /// <param name="value">The value of the claim to check for</param>
        public ClaimTypeAndValueRule(IUserSecurityActor userToAuthorize, string claimType, string value)
        {
            _userToAuthorize = userToAuthorize;
            ClaimType = claimType;
            Value = value;
        }

        /// <summary>
        /// Gets the claimtype for the rule
        /// </summary>
        public string ClaimType { get; }

        /// <summary>
        /// Gets the value of the claim for the rule
        /// </summary>
        public string Value { get;}

        /// <inheritdoc/>
        public bool IsAuthorized(object securable)
        {
            return string.IsNullOrWhiteSpace(ClaimType) || _userToAuthorize.HasClaimTypeWithValue(ClaimType, Value);
        }

        /// <inheritdoc/>
        public string Description
        {
            get { return string.Format(DescriptionFormat, ClaimType); }
        }

    }
}
