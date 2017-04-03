/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Events.Redis;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring <see cref="IEventProcessorStates"/>
    /// </summary>
    public static class EventProcessorStatesConfigurationExtensions
    {
        /// <summary>
        /// Configure <see cref="IEventProcessorStates"/> to be using Redis
        /// </summary>
        /// <param name="eventProcessorStatesConfiguration"><see cref="EventProcessorStatesConfiguration">Configuration instance</see> to configure</param>
        /// <param name="connectionStrings"></param>
        /// <returns>Chained <see cref="EventProcessorStatesConfiguration"/></returns>
        public static EventProcessorStatesConfiguration UsingRedis(this EventProcessorStatesConfiguration eventProcessorStatesConfiguration, params string[] connectionStrings)
        {
            eventProcessorStatesConfiguration.EventProcessorStates = typeof(EventProcessorStates);
            var configuration = new Redis.EventProcessorStatesConfiguration(connectionStrings);
            Configure.Instance.Container.Bind(configuration);
            return eventProcessorStatesConfiguration;
        }
    }
}
