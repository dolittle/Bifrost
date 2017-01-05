/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents the result of an authorization of a <see cref="ISecurityAction"/>
    /// </summary>
    public class AuthorizeActionResult
    {
        readonly List<AuthorizeTargetResult> _authorizationFailures = new List<AuthorizeTargetResult>();

        /// <summary>
        /// Instantiates an instance of <see cref="AuthorizeActionResult"/> for the specificed <see cref="ISecurityAction"/>
        /// </summary>
        /// <param name="action"><see cref="ISecurityAction"/> that this <see cref="AuthorizeActionResult"/> pertains to.</param>
        public AuthorizeActionResult(ISecurityAction action)
        {
            Action = action;
        }

        /// <summary>
        /// Gets the <see cref="ISecurityAction"/> that this <see cref="AuthorizeTargetResult"/> pertains to.
        /// </summary>
        public ISecurityAction Action { get; private set; }

        /// <summary>
        /// Gets the <see cref="AuthorizeTargetResult"/> for all failed <see cref="ISecurityTarget"> Actors </see> on the <see cref="ISecurable"/>
        /// </summary>
        public IEnumerable<AuthorizeTargetResult> AuthorizationFailures
        {
            get { return _authorizationFailures.AsEnumerable(); }
        }

        /// <summary>
        /// Processes an <see cref="AuthorizeTargetResult"/> for an <see cref="ISecurityTarget"> Actor</see> adding it to the AuthorizationFailures collection if appropriate
        /// </summary>
        /// <param name="result">Result to process</param>
        public void ProcessAuthorizeTargetResult(AuthorizeTargetResult result)
        {
            if (!result.IsAuthorized)
                _authorizationFailures.Add(result);
        }

        /// <summary>
        /// Gets the result of the Authorization for the <see cref="ISecurityTarget"/>
        /// </summary>
        public virtual bool IsAuthorized
        {
            get { return !_authorizationFailures.Any(); }
        }

        /// <summary>
        /// Builds a collection of strings that show Action/Target for each broken or erroring rule in <see cref="AuthorizeActionResult"/>
        /// </summary>
        /// <returns>A collection of strings</returns>
        public virtual IEnumerable<string> BuildFailedAuthorizationMessages()
        {
            return from result in AuthorizationFailures from message in result.BuildFailedAuthorizationMessages() select Action.ActionType + "/" + message;
        }
    }
}