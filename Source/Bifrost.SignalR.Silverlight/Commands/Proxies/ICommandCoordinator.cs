using System;
using System.Threading.Tasks;
using Bifrost.Commands;
using Bifrost.SignalR.Silverlight.Hubs;

namespace Bifrost.SignalR.Silverlight.Commands.Proxies
{
    public delegate void EventsProcessed(Guid commandContext);

    public interface ICommandCoordinator : IDynamicHubProxy
    {
        event EventsProcessed EventsProcessed;

        Task<CommandResult> Handle(CommandDescriptor descriptor);
    }
}
