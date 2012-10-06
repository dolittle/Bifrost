using Bifrost.Commands;
using SignalR.Hubs;

namespace Bifrost.SignalR.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;

        public CommandCoordinator(ICommandCoordinator commandCoordinator)
        {
            _commandCoordinator = commandCoordinator;
        }

        public CommandResult Handle(string commandAsJson)
        {
            return new CommandResult();
        }
    }
}
