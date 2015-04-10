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
            eventsConfiguration.EventStoreType = typeof(EventStore);
            eventsConfiguration.EventSubscriptionsType = typeof(EventSubscriptions);
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
