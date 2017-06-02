/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Read.MongoDB;
using Bifrost.Execution;
using MongoDB.Bson.Serialization;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Configuration extensions for configuring MongoDB storage
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configure the default storage mechanism to use MongoDB
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage"/> to configure</param>
        /// <param name="configureCallback">Callback to configure more details for the connections</param>
        /// <returns>Chained <see cref="IConfigure"/></returns>
        public static IConfigure UsingMongoDB(this IHaveStorage storage, Action<EntityContextConfiguration> configureCallback)
        {
            var entityContextConfiguration = new EntityContextConfiguration();
            configureCallback(entityContextConfiguration);

            var connection = new EntityContextConnection(entityContextConfiguration);
            entityContextConfiguration.Connection = connection;

            storage.EntityContextConfiguration = entityContextConfiguration;
            return Configure.Instance;
        }        

        /// <summary>
        /// Specifiy the url for the MongoDB server
        /// </summary>
        /// <param name="configuration">Fluent <see cref="EntityContextConfiguration"/></param>
        /// <param name="url">Url for the server</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/></returns>
        public static EntityContextConfiguration WithUrl(this EntityContextConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }

        /// <summary>
        /// Enable SSL for th connection to the MongoDB server
        /// </summary>
        /// <param name="configuration">Fluent <see cref="EntityContextConfiguration"/></param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/></returns>
        public static EntityContextConfiguration WithSSL(this EntityContextConfiguration configuration)
        {
            configuration.UseSSL = true;
            return configuration;
        }

        /// <summary>
        /// Set the default database for the connection
        /// </summary>
        /// <param name="configuration">Fluent <see cref="EntityContextConfiguration"/></param>
        /// <param name="defaultDatabase">Set the default database for the MongoDB connection</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/></returns>
        public static EntityContextConfiguration WithDefaultDatabase(this EntityContextConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }
    }
}
