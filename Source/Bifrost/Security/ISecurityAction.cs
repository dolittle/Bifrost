/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines an action that is subject to security
    /// </summary>
    public interface ISecurityAction
    {
        /// <summary>
        /// Add a <see cref="ISecurityTarget"/> 
        /// </summary>
        /// <param name="securityTarget"><see cref="ISecurityTarget"/> to add</param>
        void AddTarget(ISecurityTarget securityTarget);

        /// <summary>
        /// Get all <see cref="ISecurityTargets">security targets</see>
        /// </summary>
        IEnumerable<ISecurityTarget> Targets { get; }

        /// <summary>
        /// Indicates whether this action can authorize the instance of the action
        /// </summary>
        /// <param name="actionToAuthorize">An instance of the action to authorize</param>
        /// <returns>True if the <see cref="ISecurityAction"/> can authorize this action, False otherwise</returns>
        bool CanAuthorize(object actionToAuthorize);

        /// <summary>
        /// Authorizes this <see cref="ISecurityAction"/> for the instance of the action
        /// </summary>
        /// <param name="actionToAuthorize">Instance of an action to be authorized</param>
        /// <returns>An <see cref="AuthorizeActionResult"/> that indicates if the action was authorized or not</returns>
        AuthorizeActionResult Authorize(object actionToAuthorize);

        /// <summary>
        /// Returns a string description of this <see cref="ISecurityAction"/>
        /// </summary>
        string ActionType { get; }
    }
}
