/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Events.Redis;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring Redis support for <see cref="IEventSequenceNumbers"/>
    /// </summary>
    public static class EventSourceVersionsConfigurationExtensions
    {
        /// <summary>
        /// Configure <see cref="IEventSequenceNumbers"/> to be using Redis
        /// </summary>
        /// <param name="eventSourceVersionsConfiguration"><see cref="EventSequenceConfiguration">Configuration instance</see> to configure</param>
        /// <param name="connectionStrings"><see cref="string">string or strings</see> representing connectionstrings according to StackExchange.Redis</param>
        /// <returns>Chained <see cref="EventSequenceConfiguration"/></returns>
        public static EventSourceVersionsConfiguration UsingRedis(this EventSourceVersionsConfiguration eventSourceVersionsConfiguration, params string[] connectionStrings)
        {
            eventSourceVersionsConfiguration.EventSourceVersions = typeof(EventSourceVersions);
            var configuration = new Redis.EventSourceVersionsConfiguration(connectionStrings);
            Configure.Instance.Container.Bind(configuration);
            return eventSourceVersionsConfiguration;
        }
    }
}
