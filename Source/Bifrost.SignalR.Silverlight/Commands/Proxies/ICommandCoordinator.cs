using Bifrost.Commands;
using Bifrost.SignalR.Silverlight.Hubs;
using System.Threading.Tasks;

namespace Bifrost.SignalR.Silverlight.Commands.Proxies
{
    public interface ICommandCoordinator : IDynamicHubProxy
    {
        Task<CommandResult> Handle(CommandDescriptor descriptor);
    }
}
