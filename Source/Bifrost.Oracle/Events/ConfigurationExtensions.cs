using System;
using Bifrost.Oracle.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingOracleEventStore(this IEventsConfiguration eventsConfiguration, Action<EventStoreConfiguration> configureCallback)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            var configuration = new EventStoreConfiguration();
            configureCallback(configuration);

            Configure.Instance.Container.Bind(configuration);
            return Configure.Instance;
        }

        public static IConfigure UsingOracleEventStore(this IEventsConfiguration eventsConfiguration, string connectionString, Action<EventStoreConfiguration> configureCallback = null)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            var configuration = new EventStoreConfiguration
            {
                Connection = new OracleConnection(connectionString)
            };
            if (configureCallback != null)
                configureCallback(configuration);

            Configure.Instance.Container.Bind(configuration);
            return Configure.Instance;
        }

    }
}
