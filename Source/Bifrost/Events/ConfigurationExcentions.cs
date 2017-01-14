/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Events.InProcess;

namespace Bifrost.Events
{
    /// <summary>
    /// Provides extensions for <see cref="IEventsConfiguration">configuration</see>
    /// </summary>
    public static class ConfigurationExcentions
    {
        /// <summary>
        /// Configures events to be persisted synchronously
        /// </summary>
        /// <param name="configuration"><see cref="IEventsConfiguration"/> instance to configure</param>
        /// <param name="configurationAction">Callback for further configuring the <see cref="IEventsConfiguration"/></param>
        /// <returns>Chained <see cref="IConfigure"/> instance</returns>
        public static IConfigure Synchronous(this IEventsConfiguration configuration, Action<IEventsConfiguration> configurationAction = null)
        {
            configuration.UncommittedEventStreamCoordinator = typeof(UncommittedEventStreamCoordinator);
            configuration.CommittedEventStreamSender = typeof(CommittedEventStreamSender);
            configuration.CommittedEventStreamReceiver = typeof(CommittedEventStreamReceiver);
            configurationAction?.Invoke(configuration);
            return Configure.Instance;
        }
    }
}
