/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Reflection;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents the handler for <see cref="ICommandProcess"/>
    /// </summary>
    public class CommandProcessHandler : ICommandProcess, INeedProxyInstance
    {

#pragma warning disable 1591 // Xml Comments

        public void Succeeded(CommandSucceeded callback)
        {
            ((ICanProcessCommandProcess)Proxy).AddSucceeded(callback);
        }

        public void Failed(CommandFailed callback)
        {
            ((ICanProcessCommandProcess)Proxy).AddFailed(callback);
        }

        public void Handled(CommandHandled callback)
        {
            ((ICanProcessCommandProcess)Proxy).AddHandled(callback);
        }

        public object Proxy { get; set; }
#pragma warning restore 1591 // Xml Comments


    }
}
