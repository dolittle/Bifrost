/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
