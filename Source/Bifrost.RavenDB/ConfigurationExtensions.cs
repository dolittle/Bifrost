#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using System.Net;
using Bifrost.RavenDB;
using Bifrost.RavenDB.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingRavenDB(this IEventsConfiguration eventsConfiguration, Action<EventStoreConfiguration> configureCallback)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            var configuration = new EventStoreConfiguration();
            configureCallback(configuration);
            Configure.Instance.Container.Bind<EventStoreConfiguration>(configuration);
            return Configure.Instance;
        }

        public static EventStoreConfiguration WithUrl(this EventStoreConfiguration configuration, string url)
        {
            configuration.Url = url;
            return configuration;
        }

        public static EventStoreConfiguration WithCredentials(this EventStoreConfiguration configuration, ICredentials credentials)
        {
            configuration.Credentials = credentials;
            return configuration;
        }

        public static EventStoreConfiguration WithDefaultDatabase(this EventStoreConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }

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
