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
        /// <param name="eventStoreConfiguration"><see cref="Events.EventStoreConfiguration"/> to configure</param>
        /// <param name="path">Path to where the event store should live</param>
        /// <returns>Chained <see cref="Events.EventStoreConfiguration"/> for fluent configuration</returns>
        public static Events.EventStoreConfiguration UsingFiles(this Events.EventStoreConfiguration eventStoreConfiguration, string path)
        {
            eventStoreConfiguration.EventStore = typeof(EventStore);

            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new Events.Files.EventStoreConfiguration
            {
                Path = path
            };
            Configure.Instance.Container.Bind(configuration);

            return eventStoreConfiguration;
        }

        /// <summary>
        /// Configures the <see cref="Events.EventSequenceNumbersConfiguration"/>
        /// </summary>
        /// <param name="eventSequenceConfiguration"><see cref="Events.EventSequenceNumbersConfiguration">Configuration instance</see> to configure</param>
        /// <param name="path">Path to where to store <see cref="IEventSequenceNumbers">event sequence numbers</see></param>
        /// <returns>Chained <see cref="Events.EventSequenceNumbersConfiguration"/></returns>
        public static Events.EventSequenceNumbersConfiguration UsingFiles(this Events.EventSequenceNumbersConfiguration eventSequenceConfiguration, string path)
        {
            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new Events.Files.EventSequenceNumbersConfiguration
            {
                Path = path
            };
            Configure.Instance.Container.Bind(configuration);

            eventSequenceConfiguration.EventSequenceNumbers = typeof(EventSequenceNumbers);

            return eventSequenceConfiguration;
        }

        /// <summary>
        /// Configures the <see cref="Events.EventSequenceNumbersConfiguration"/>
        /// </summary>
        /// <param name="eventProcessorStatesConfiguration"><see cref="Events.EventProcessorStatesConfiguration">Configuration instance</see> to configure</param>
        /// <param name="path">Path to where to store <see cref="IEventProcessorState">event processor state</see></param>
        /// <returns>Chained <see cref="Events.EventProcessorStatesConfiguration"/></returns>
        public static Events.EventProcessorStatesConfiguration UsingFiles(this Events.EventProcessorStatesConfiguration eventProcessorStatesConfiguration, string path)
        {
            if (!Path.IsPathRooted(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), path);

            var configuration = new Events.Files.EventProcessorStatesConfiguration
            {
                Path = path
            };
            Configure.Instance.Container.Bind(configuration);

            eventProcessorStatesConfiguration.EventProcessorStates = typeof(EventProcessorStates);

            return eventProcessorStatesConfiguration;
        }


        /// <summary>
        /// Configures <see cref="EventSourceVersionsConfiguration"/> for a file representation
        /// </summary>
        /// <param name="eventSourceVersionsConfiguration"><see cref="EventSourceVersionsConfiguration">Configuration instance</see> to configure</param>
        /// <param name="path">Path to where to store <see cref="EventSourceVersion"/> per <see cref="IEventSource"/></param>
        /// <returns>Chained <see cref="EventSourceVersionsConfiguration"/></returns>
        public static EventSourceVersionsConfiguration UsingFiles(this EventSourceVersionsConfiguration eventSourceVersionsConfiguration, string path)
        {
            Configure.Instance.Container.Bind<ICanProvideEventSourceVersionsPath>(() => path);

            return eventSourceVersionsConfiguration;
        }
    }
}
