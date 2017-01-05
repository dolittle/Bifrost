/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.DocumentDB;
using Bifrost.DocumentDB.Entities;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuration
    /// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Configures <see cref="IHaveStorage">storage</see> to use a DocumentDB
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage">Storage</see> to configure</param>
        /// <param name="callback">Chained callback for configuring the specifics</param>
        /// <returns>Chained <see cref="IConfigure"/> for fluent configuration</returns>
        public static IConfigure UsingDocumentDB(this IHaveStorage storage, Action<EntityContextConfiguration> callback)
        {
            var configuration = new EntityContextConfiguration();
            callback(configuration);

            var collectionStrategy = new MultipleEntitiesInOneCollection();

            var connection = new EntityContextConnection(configuration, collectionStrategy);
            configuration.Connection = connection;

            storage.EntityContextConfiguration = configuration;

            Configure.Instance.Container.Bind<ICollectionStrategy>(collectionStrategy);

            return Configure.Instance;
        }


        /// <summary>
        /// Configure the Url endpoint for the database server
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration"/> to configure</param>
        /// <param name="url"></param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/> to configure</returns>
        public static EntityContextConfiguration WithUrl(this EntityContextConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }

        /// <summary>
        /// Configure the default database by its databaseId
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration"/> to configure</param>
        /// <param name="databaseId">Database id to connect to</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/> to configure</returns>
        public static EntityContextConfiguration WithDefaultDatabase(this EntityContextConfiguration configuration, string databaseId)
        {
            configuration.DatabaseId = databaseId;
            return configuration;
        }

        /// <summary>
        /// Configure the authorization key to use
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration"/> to configure</param>
        /// <param name="authorizationKey">Authorization key to use</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/> to configure</returns>
        public static EntityContextConfiguration UsingAuthorizationKey(this EntityContextConfiguration configuration, string authorizationKey)
        {
            configuration.AuthorizationKey = authorizationKey;
            return configuration;
        }
    }
}
