using System.Web.Routing;
using Bifrost.SignalR.Commands;
using Bifrost.SignalR.Events;
using SignalR;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this IEventsConfiguration configuration)
        {
            configuration.AddEventStoreChangeNotifier(typeof(EventStoreChangeNotifier));
            // MapConnection<CommandCoordinatorHub>("CommandCoordinator", "PersistentConnections/CommandCoordinator/{*operation}");
            return Configure.Instance;
        }
    }
}
