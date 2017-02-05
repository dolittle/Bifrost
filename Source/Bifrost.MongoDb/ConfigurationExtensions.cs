/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.MongoDB;
using Bifrost.MongoDB.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingMongoDB(this IEventsConfiguration eventsConfiguration, Action<EventStorageConfiguration> configureCallback)
        {
            eventsConfiguration.EventStore = typeof(EventStore);
            eventsConfiguration.EventSubscriptions = typeof(EventSubscriptions);
            var configuration = new EventStorageConfiguration();
            configureCallback(configuration);
            Configure.Instance.Container.Bind<EventStorageConfiguration>(configuration);
            return Configure.Instance;
        }

        public static EventStorageConfiguration WithUrl(this EventStorageConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }


        public static EventStorageConfiguration WithDefaultDatabase(this EventStorageConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }

        public static IConfigure UsingMongoDB(this IHaveStorage storage, Action<EntityContextConfiguration> configureCallback)
        {
            var entityContextConfiguration = new EntityContextConfiguration();
            configureCallback(entityContextConfiguration);

            var connection = new EntityContextConnection(entityContextConfiguration);
            entityContextConfiguration.Connection = connection;

            storage.EntityContextConfiguration = entityContextConfiguration;
            return Configure.Instance;
        }


        public static EntityContextConfiguration WithUrl(this EntityContextConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }


        public static EntityContextConfiguration WithDefaultDatabase(this EntityContextConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }


    }
}
