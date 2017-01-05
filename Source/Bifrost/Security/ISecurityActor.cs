/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Defines the security object that defines the security
    /// </summary>
    public interface ISecurityActor
    {
        /// <summary>
        /// Authorizes an instance of an action
        /// </summary>
        /// <param name="actionToAuthorize">An instance of the action to authorize</param>
        /// <returns>A <see cref="AuthorizeActorResult"/> which encapsulates the result of the authorization attempt of this instance of an action</returns>
        AuthorizeActorResult IsAuthorized(object actionToAuthorize);

        /// <summary>
        /// Gets a description of the SecurityActor.
        /// </summary>
        string Description { get; }
    }
}
