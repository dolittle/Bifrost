/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

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
        /// <param name="eventsConfiguration"></param>
        /// <returns></returns>
        public static IConfigure Eventing(this IConfigure configure, Action<IEventsConfiguration> eventsConfiguration)
        {
            eventsConfiguration(configure.Events);
            return configure;
        }
    }
}
