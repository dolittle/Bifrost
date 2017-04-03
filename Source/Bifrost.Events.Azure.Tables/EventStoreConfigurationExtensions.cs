/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Events.Azure.Tables;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring <see cref="EventStoreConfiguration"/>
    /// </summary>
    public static class EventStoreConfigurationExtensions
    {
        /// <summary>
        /// Configures <see cref="IEventStore"/> to be using <see cref="EventStore">Azure Tables</see>
        /// </summary>
        /// <param name="eventStoreConfiguration"><see cref="EventStoreConfiguration"/> to configure</param>
        /// <param name="connectionString"><see cref="string">ConnectionString</see> for connecting to your Azure Storage account</param>
        /// <returns>Chained <see cref="EventStoreConfiguration"/></returns>
        public static EventStoreConfiguration UsingTables(this EventStoreConfiguration eventStoreConfiguration, string connectionString)
        {
            eventStoreConfiguration.EventStore = typeof(EventStore);
            Configure.Instance.Container.Bind<ICanProvideConnectionString>(() => connectionString);
            return eventStoreConfiguration;
        }
    }
}
