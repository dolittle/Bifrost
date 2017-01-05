/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ISecurityAction"/> for handling <see cref="ICommand">commands</see>
    /// </summary>
    public class HandleCommand : SecurityAction
    {
#pragma warning disable 1591 // Xml Comments
        public override string ActionType
        {
            get { return "Handle"; }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
