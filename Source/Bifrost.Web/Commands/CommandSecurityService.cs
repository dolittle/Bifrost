/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Security;

namespace Bifrost.Web.Commands
{
    public class CommandSecurityService
    {
        readonly ICommandSecurityManager _commandSecurityManager;
        readonly ITypeDiscoverer _typeDiscoverer;

        public CommandSecurityService(ICommandSecurityManager commandSecurityManager, ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
            _commandSecurityManager = commandSecurityManager;
        }

        public AuthorizationResult GetForCommand(string commandName)
        {
            var commandType = _typeDiscoverer.GetCommandTypeByName(commandName);
            var commandInstance = Activator.CreateInstance(commandType) as ICommand;
            var result = _commandSecurityManager.Authorize(commandInstance);
            return result;
        }
    }
}
