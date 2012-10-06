using Bifrost.Commands;
using SignalR.Hubs;
using Bifrost.Serialization;
using System.Threading.Tasks;
using System.Threading;

namespace Bifrost.SignalR.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;
        ICommandTypeManager _commandTypeManager;
        ISerializer _serializer;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ICommandTypeManager commandTypeManager,
            ISerializer serializer
            )
        {
            _commandCoordinator = commandCoordinator;
            _commandTypeManager = commandTypeManager;
            _serializer = serializer;
        }

        public CommandResult Handle(CommandDescriptor descriptor)
        {
            var commandType = _commandTypeManager.GetFromName(descriptor.Name);
            var command = (ICommand)_serializer.FromJson(commandType, descriptor.Command);
            var commandResult = _commandCoordinator.Handle(command);
            return commandResult;
        }
    }
}
