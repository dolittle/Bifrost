/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Net;
using Bifrost.RavenDB;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {

        public static IConfigure UsingRavenDB(this IHaveStorage storage, Action<EntityContextConfiguration> configureCallback = null)
        {
            var entityContextConfiguration = new EntityContextConfiguration
            {
                IdPropertyRegister = new NullIdPropertyRegister()
            };
            if (configureCallback != null)
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

        public static EntityContextConfiguration WithCredentials(this EntityContextConfiguration configuration, ICredentials credentials)
        {
            configuration.Credentials = credentials;
            return configuration;
        }

        public static EntityContextConfiguration WithDefaultDatabase(this EntityContextConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }

    }
}
