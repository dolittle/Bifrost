using SignalR.Client;
using SignalR.Client.Hubs;

namespace Bifrost.SignalR.Silverlight.Hubs
{
    public interface IHubProxyManager
    {
        T Get<T>() where T : IDynamicHubProxy;
    }
}
