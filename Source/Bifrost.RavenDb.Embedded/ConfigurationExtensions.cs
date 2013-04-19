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
using Bifrost.Entities;
using Bifrost.RavenDB;
using Bifrost.RavenDB.Embedded.Events;
using EntityContextConfiguration = Bifrost.RavenDB.Embedded.EntityContextConfiguration;
using EventStoreConfiguration = Bifrost.RavenDB.Embedded.Events.EventStoreConfiguration;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingRavenDBEmbedded(this IHaveStorage storage, Action<EntityContextConfiguration> configureCallback)
        {
            var entityContextConfiguration = new EntityContextConfiguration
            {
                IdPropertyRegister = new NullIdPropertyRegister()
            };
            if (configureCallback != null)
                configureCallback(entityContextConfiguration);

            var connection = new Bifrost.RavenDB.EntityContextConnection(entityContextConfiguration);
            entityContextConfiguration.Connection = connection;
            storage.EntityContextConfiguration = entityContextConfiguration;

            return Configure.Instance;
        }

        public static IConfigure UsingRavenDBEmbedded(this IEventsConfiguration eventsConfiguration, Action<EventStoreConfiguration> configureCallback)
        {
            eventsConfiguration.EventStoreType = typeof(Bifrost.RavenDB.Events.EventStore);
            var configuration = new EventStoreConfiguration();
            configureCallback(configuration);
            Configure.Instance.Container.Bind<Bifrost.RavenDB.Events.EventStoreConfiguration>(configuration);
            return Configure.Instance;
        }

        public static EventStoreConfiguration LocatedAt(this EventStoreConfiguration configuration, string path)
        {
            configuration.DataDirectory = path;
            return configuration;
        }

        public static EntityContextConfiguration LocatedAt(this EntityContextConfiguration configuration, string path)
        {
            configuration.DataDirectory = path;
            return configuration;
        }

        public static EventStoreConfiguration WithManagementStudio(this EventStoreConfiguration configuration, int port = 8080)
        {
            configuration.EnableManagementStudio = true;
            configuration.ManagementStudioPort = port;
            return configuration;
        }

        public static EntityContextConfiguration WithManagementStudio(this EntityContextConfiguration configuration, int port = 8080)
        {
            configuration.EnableManagementStudio = true;
            configuration.ManagementStudioPort = port;
            return configuration;
        }
    }
}
