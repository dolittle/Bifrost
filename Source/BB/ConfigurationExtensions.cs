/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Read.MongoDB;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Configuration extensions for configuring MongoDB storage
    /// </summary>
    public static class ConfigurationExtensions
    {

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
