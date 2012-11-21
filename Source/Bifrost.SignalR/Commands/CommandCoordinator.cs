using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Serialization;
using SignalR.Hubs;
using System;
using SignalR;
using System.Threading;
using System.Threading.Tasks;

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
            var commandType = _commandTypeManager.GetFromName(descriptor.Name);
            var command = (ICommand)_serializer.FromJson(commandType, descriptor.Command);
            command.Id = descriptor.Id;
            _commandContextConnectionManager.Register(Context.ConnectionId, command.Id);
            var commandResult = _commandCoordinator.Handle(command);
            return commandResult;
        }

        public static void EventsProcessed(string connectionId, Guid commandContext)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CommandCoordinator>();
            context.Clients.EventsProcessed(commandContext);
        }
    }
}
