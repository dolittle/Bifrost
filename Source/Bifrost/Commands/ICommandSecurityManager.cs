/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a manager for dealing with security for <see cref="ICommand">commands</see>
    /// </summary>
    public interface ICommandSecurityManager
    {
        /// <summary>
        /// Authorizes a <see cref="ICommand"/> 
        /// </summary>
        /// <param name="command"><see cref="ICommand"/> to ask for</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="ICommand"/> was authorized</returns>
        AuthorizationResult Authorize(ICommand command);
    }
}
