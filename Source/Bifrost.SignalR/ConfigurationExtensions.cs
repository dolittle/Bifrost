using System.Web.Routing;
using Bifrost.SignalR;
using Bifrost.SignalR.Events;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this IEventsConfiguration configuration)
        {
            GlobalHost.DependencyResolver = new BifrostDependencyResolver(Configure.Instance.Container);
            RouteTable.Routes.MapHubs();
            configuration.AddEventStoreChangeNotifier(typeof(EventStoreChangeNotifier));
            return Configure.Instance;
        }
    }
}
