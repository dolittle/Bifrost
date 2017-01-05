/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandSecurityManager"/>
    /// </summary>
    public class CommandSecurityManager : ICommandSecurityManager
    {
        ISecurityManager _securityManager;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandSecurityManager"/>
        /// </summary>
        /// <param name="securityManager"><see cref="ISecurityManager"/> for forwarding requests related to security to</param>
        public CommandSecurityManager(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

#pragma warning disable 1591 // Xml Comments
        public AuthorizationResult Authorize(ICommand command)
        {
            return _securityManager.Authorize<HandleCommand>(command);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
