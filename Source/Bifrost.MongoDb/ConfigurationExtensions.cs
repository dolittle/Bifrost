#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
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
using Bifrost.MongoDB;
using Bifrost.MongoDB.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingMongoDB(this IEventsConfiguration eventsConfiguration, Action<EventStoreConfiguration> configureCallback)
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


        public static EventStoreConfiguration WithDefaultDatabase(this EventStoreConfiguration configuration, string defaultDatabase)
        {
            configuration.DefaultDatabase = defaultDatabase;
            return configuration;
        }

        public static IConfigure UsingMongoDB(this IHaveStorage storage, string connectionString, string databaseName)
        {
            var entityContextConfiguration = new EntityContextConfiguration();
            var connection = new EntityContextConnection(connectionString, databaseName);
            entityContextConfiguration.Connection = connection;

            storage.EntityContextConfiguration = entityContextConfiguration;
            return Configure.Instance;
        }

    }
}
