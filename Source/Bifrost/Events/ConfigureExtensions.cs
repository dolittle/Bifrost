/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for <see cref="IConfigure"/> 
    /// </summary>
    public static partial class ConfigureExtensions
    {
        /// <summary>
        /// Confugre eventing
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="eventsConfigurationCallback"></param>
        /// <returns></returns>
        public static IConfigure Events(this IConfigure configure, Action<IEventsConfiguration> eventsConfigurationCallback)
        {
            var eventsConfiguration = new EventsConfiguration();
            configure.Container.Bind<IEventsConfiguration>(eventsConfiguration);
            eventsConfigurationCallback(eventsConfiguration);
            return configure;
        }
    }
}
