#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using Bifrost.MongoDB;
using Bifrost.MongoDB.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingMongoDB(this IEventsConfiguration eventsConfiguration, Action<EventStorageConfiguration> configureCallback)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            eventsConfiguration.EventSubscriptionsType = typeof(EventSubscriptions);
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
