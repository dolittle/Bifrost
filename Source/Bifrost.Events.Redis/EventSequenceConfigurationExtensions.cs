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
    public static class EventSequenceConfigurationExtensions
    {
        /// <summary>
        /// Configure <see cref="IEventSequenceNumbers"/> to be using Redis
        /// </summary>
        /// <param name="eventSequenceConfiguration"><see cref="EventSequenceConfiguration">Configuration instance</see> to configure</param>
        /// <param name="connectionStrings"></param>
        /// <returns>Chained <see cref="EventSequenceConfiguration"/></returns>
        public static EventSequenceConfiguration UsingRedis(this EventSequenceConfiguration eventSequenceConfiguration, params string[] connectionStrings)
        {
            eventSequenceConfiguration.EventSequenceNumbers = typeof(EventSequenceNumbers);
            var configuration = new EventSequenceNumbersConfiguration(connectionStrings);
            Configure.Instance.Container.Bind(configuration);
            return eventSequenceConfiguration;
        }
    }
}
