using Bifrost.Commands;
using Bifrost.Serialization;
using SignalR.Hubs;
using System;

namespace Bifrost.SignalR.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;
        ICommandTypeManager _commandTypeManager;
        ICommandContextConnectionManager _commandContextConnectionManager;
        ISerializer _serializer;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ICommandTypeManager commandTypeManager,
            ICommandContextConnectionManager commandContextConnectionManager,
            ISerializer serializer)
        {
            _commandCoordinator = commandCoordinator;
            _commandTypeManager = commandTypeManager;
            _commandContextConnectionManager = commandContextConnectionManager;
            _serializer = serializer;
        }

        public CommandResult Handle(CommandDescriptor descriptor)
        {
            try
            {
                var commandType = _commandTypeManager.GetFromName(descriptor.Name);
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
