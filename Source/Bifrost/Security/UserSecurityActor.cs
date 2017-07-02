/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;

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

        /// <inheritdoc/>
        public bool IsInRole(string role)
        {
            return _resolvePrincipal.Resolve().IsInRole(role);
        }

        /// <inheritdoc/>
        public bool HasClaimType(string claimType)
        {
            return _resolvePrincipal.Resolve().FindAll(claimType).Count() > 0;
        }

        /// <inheritdoc/>
        public bool HasClaimTypeWithValue(string claimType, string value)
        {
            return _resolvePrincipal.Resolve().Claims.Any(_ => _.Type == claimType && _.Value == value);
        }
    }
}
