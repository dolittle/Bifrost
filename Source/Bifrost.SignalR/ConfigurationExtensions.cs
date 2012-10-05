using Bifrost.SignalR.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this IEventsConfiguration configuration)
        {
            configuration.AddEventStoreChangeNotifier(typeof(EventStoreChangeNotifier));
            return Configure.Instance;
        }
    }
}
