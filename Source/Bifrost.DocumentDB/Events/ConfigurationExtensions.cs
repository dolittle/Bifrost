/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.DocumentDB.Events;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuration
    /// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Configures events to be stored in an Azure DocumentDB
        /// </summary>
        /// <param name="eventsConfiguration"><see cref="IEventsConfiguration"/> being fluently configured</param>
        /// <param name="configureCallback"><see cref="Action{EventStorageConfiguration}">Callback</see> to get called for configuration</param>
        /// <returns></returns>
        public static IConfigure UsingDocumentDB(this IEventsConfiguration eventsConfiguration, Action<EventStorageConfiguration> configureCallback)
        {
            eventsConfiguration.EventStore = typeof(EventStore);
            var configuration = new EventStorageConfiguration();
            configureCallback(configuration);
            Configure.Instance.Container.Bind<EventStorageConfiguration>(configuration);
            return Configure.Instance;
        }

        /// <summary>
        /// Configure the Url endpoint for the database server
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration"/> to configure</param>
        /// <param name="url"></param>
        /// <returns>Chained <see cref="EventStorageConfiguration"/> to configure</returns>
        public static EventStorageConfiguration WithUrl(this EventStorageConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }

        /// <summary>
        /// Configure the default database by its databaseId
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration"/> to configure</param>
        /// <param name="databaseId">Database id to connect to</param>
        /// <returns>Chained <see cref="EventStorageConfiguration"/> to configure</returns>
        public static EventStorageConfiguration WithDefaultDatabase(this EventStorageConfiguration configuration, string databaseId)
        {
            configuration.DatabaseId = databaseId;
            return configuration;
        }

        /// <summary>
        /// Configure the authorization key to use
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration"/> to configure</param>
        /// <param name="authorizationKey">Authorization key to use</param>
        /// <returns>Chained <see cref="EventStorageConfiguration"/> to configure</returns>
        public static EventStorageConfiguration UsingAuthorizationKey(this EventStorageConfiguration configuration, string authorizationKey)
        {
            configuration.AuthorizationKey = authorizationKey;
            return configuration;
        }

    }
}
