#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.IO;
using Bifrost.Events;
using Bifrost.Events.Files;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuration
    /// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Configures the <see cref="IEventStore"/>
        /// </summary>
        /// <param name="eventsConfiguration"><see cref="IEventsConfiguration"/> to configure</param>
        /// <param name="path">Path to where the event store should live</param>
        /// <returns>Chained <see cref="IConfigure"/> for fluent configuration</returns>
        public static IConfigure UsingFiles(this IEventsConfiguration eventsConfiguration, string path)
        {
            eventsConfiguration.EventStoreType = typeof(EventStore);
            eventsConfiguration.EventSubscriptionsType = typeof(EventSubscriptions);
            eventsConfiguration.UncommittedEventStreamCoordinatorType = typeof(UncommittedEventStreamCoordinator);

            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new EventStoreConfiguration
            {
                Path = path
            };
            Configure.Instance.Container.Bind<EventStoreConfiguration>(configuration);

            return Configure.Instance;
        }
    }
}
