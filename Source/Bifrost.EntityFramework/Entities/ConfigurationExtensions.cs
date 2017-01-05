/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.EntityFramework.Entities;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Provides fluent configuration extensions for EntityFramework specifics related to entities
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Use EntityFramework as <see cref="IHaveStorage">storage</see>
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage"/> to configure</param>
        /// <param name="connectionConfiguration">Callback for configuration the connection</param>
        /// <returns></returns>
        public static IConfigure UsingEntityFramework(this IHaveStorage storage, Action<EntityContextConfiguration> connectionConfiguration)
        {
            var configuration = new EntityContextConfiguration();
            connectionConfiguration(configuration);

            var connection = new EntityContextConnection(configuration);
            configuration.Connection = connection;

            storage.EntityContextConfiguration = configuration;
            
            return Configure.Instance;
        }

        /// <summary>
        /// Configure with a given connection string
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration"/> to configure</param>
        /// <param name="connectionString">Connection string to use</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/> to configure</returns>
        public static EntityContextConfiguration WithConnectionString(this EntityContextConfiguration configuration, string connectionString)
        {
            configuration.ConnectionString = connectionString;
            return configuration;
        }
    }
}
