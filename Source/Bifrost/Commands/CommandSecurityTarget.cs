/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityTarget"/> for <see cref="ICommand">commands</see>
    /// </summary>
    public class CommandSecurityTarget : SecurityTarget
    {
        const string COMMAND = "Command";

        /// <summary>
        /// Instantiates an instance of <see cref="CommandSecurityTarget"/>
        /// </summary>
        public CommandSecurityTarget() : base(COMMAND)
        {
        }
    }
}
