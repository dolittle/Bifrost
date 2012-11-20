using System.Web.Routing;
using Bifrost.SignalR.Commands;
using Bifrost.SignalR.Events;
using SignalR;
using Bifrost.SignalR;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this IEventsConfiguration configuration)
        {
            RouteTable.Routes.MapHubs(new BifrostDependencyResolver(Configure.Instance.Container));
            configuration.AddEventStoreChangeNotifier(typeof(EventStoreChangeNotifier));
            return Configure.Instance;
        }
    }
}
