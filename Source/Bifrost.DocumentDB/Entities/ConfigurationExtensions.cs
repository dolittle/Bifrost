#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Configuration;
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

            var connection = new EntityContextConnection(configuration);
            configuration.Connection = connection;

            storage.EntityContextConfiguration = configuration;
            
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
