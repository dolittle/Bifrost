/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Serialization;

namespace Bifrost.Web.Commands
{
    public class CommandCoordinatorService
    {
        readonly ICommandCoordinator _commandCoordinator;
        readonly ISerializer _serializer;
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ISagaLibrarian _sagaLibrarian;

        public CommandCoordinatorService(
            ICommandCoordinator commandCoordinator, 
            ISerializer serializer,
            ITypeDiscoverer typeDiscoverer,
            ISagaLibrarian sagaLibrarian)
        {
            _commandCoordinator = commandCoordinator;
            _serializer = serializer;
            _typeDiscoverer = typeDiscoverer;
            _sagaLibrarian = sagaLibrarian;
        }

        public CommandResult Handle(CommandDescriptor commandDescriptor)
        {
            var commandInstance = GetCommandFromDescriptor(commandDescriptor);
            if (commandInstance == null)
                return new CommandResult { Exception = new UnknownCommandException(commandDescriptor.Name) };

            var result = _commandCoordinator.Handle(commandInstance);
            return result;
        }

        public IEnumerable<CommandResult> HandleMany(IEnumerable<CommandDescriptor> commandDescriptors)
        {
            var results = new List<CommandResult>();
            foreach (var commandDescriptor in commandDescriptors)
            {
                ICommand commandInstance = null;
                try
                {
                    commandInstance = GetCommandFromDescriptor(commandDescriptor);
                    if (commandInstance == null)
                        results.Add(new CommandResult { Exception = new UnknownCommandException(commandDescriptor.Name) });
                    else 
                        results.Add(_commandCoordinator.Handle(commandInstance));
                }
                catch (Exception ex)
                {
                    var commandResult = CommandResult.ForCommand(commandInstance);
                    commandResult.Exception = ex;
                    return new[] { commandResult };
                }
            }

            return results.ToArray();
        }


        public IEnumerable<CommandResult> HandleForSaga(Guid sagaId, CommandDescriptor[] commandDescriptors)
        {
            var results = new List<CommandResult>();
            var saga = _sagaLibrarian.Get(sagaId);

            // Todo : IMPORTANT : We need to treat this as a unit of work with rollbacks if one or more commands fail and some succeed!!!!!!!!!!! 
            foreach (var commandDescriptor in commandDescriptors)
            {
                ICommand commandInstance = null;
                try
                {
                    commandInstance = GetCommandFromDescriptor(commandDescriptor);
                    results.Add(_commandCoordinator.Handle(saga, commandInstance));
                }
                catch (Exception ex)
                {
                    var commandResult = CommandResult.ForCommand(commandInstance);
                    commandResult.Exception = ex;
                    return new[] { commandResult };
                }
            }

            return results.ToArray();
        }

        ICommand GetCommandFromDescriptor(CommandDescriptor commandDescriptor)
        {
            var commandType = _typeDiscoverer.GetCommandTypeByName(commandDescriptor.GeneratedFrom);
            var commandInstance = _serializer.FromJson(commandType, commandDescriptor.Command) as ICommand;
            return commandInstance;
        }
    }
}
