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
using Bifrost.Oracle.Events;
using Oracle.DataAccess.Client;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingOracleEventStore(this IEventsConfiguration eventsConfiguration, Action<EventStoreConfiguration> configureCallback)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            var configuration = new EventStoreConfiguration();
            configureCallback(configuration);

            Configure.Instance.Container.Bind(configuration);
            return Configure.Instance;
        }

        public static IConfigure UsingOracleEventStore(this IEventsConfiguration eventsConfiguration, string connectionString, Action<EventStoreConfiguration> configureCallback = null)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            var configuration = new EventStoreConfiguration
            {
                Connection = new OracleConnection(connectionString)
            };
            if (configureCallback != null)
                configureCallback(configuration);

            Configure.Instance.Container.Bind(configuration);
            return Configure.Instance;
        }

    }
}
