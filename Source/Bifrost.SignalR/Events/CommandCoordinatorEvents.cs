using System;
using Bifrost.SignalR.Commands;
using SignalR;

namespace Bifrost.SignalR.Events
{
    public class CommandCoordinatorEvents : ICommandCoordinatorEvents
    {
        ICommandContextConnectionManager _commandContextConnectionManager;

        public CommandCoordinatorEvents(ICommandContextConnectionManager commandContextConnectionManager)
        {
            _commandContextConnectionManager = commandContextConnectionManager;
        }

        public void EventsProcessed(Guid commandContext)
        {
            if (_commandContextConnectionManager.HasConnectionForCommandContext(commandContext))
            {
                var connectionId = _commandContextConnectionManager.GetConnectionForCommandContext(commandContext);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommandCoordinator>();
                hubContext.Clients[connectionId].EventsProcessed(commandContext);
            }
        }
    }
}
