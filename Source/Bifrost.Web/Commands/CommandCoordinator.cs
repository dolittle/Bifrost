/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Serialization;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Web.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;
        ITypeDiscoverer _typeDiscoverer;
        ICommandContextConnectionManager _commandContextConnectionManager;
        ISerializer _serializer;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ITypeDiscoverer typeDiscoverer,
            ICommandContextConnectionManager commandContextConnectionManager,
            ISerializer serializer)
        {
            _commandCoordinator = commandCoordinator;
            _typeDiscoverer = typeDiscoverer;
            _commandContextConnectionManager = commandContextConnectionManager;
            _serializer = serializer;
        }

        public CommandResult Handle(CommandDescriptor descriptor)
        {
            try
            {
                var commandType = _typeDiscoverer.GetCommandTypeByName(descriptor.GeneratedFrom);
                var command = (ICommand)_serializer.FromJson(commandType, descriptor.Command);
                command.Id = descriptor.Id;
                _commandContextConnectionManager.Register(Context.ConnectionId, command.Id);
                var commandResult = _commandCoordinator.Handle(command);
                return commandResult;
            }
            catch (Exception ex)
            {
                return new CommandResult { 
                    Exception = ex, 
                    ExceptionMessage = string.Format("Exception occured of type '{0}' with message '{1}'. StackTrace : {2}",ex.GetType().Name,ex.Message,ex.StackTrace)
                };
            }
        }
    }
}
