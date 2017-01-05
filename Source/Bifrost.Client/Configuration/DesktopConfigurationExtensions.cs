/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Extensions for configuring desktop client type
    /// </summary>
    public static class DesktopConfigurationExtensions
    {
        /// <summary>
        /// Configure frontend for the Windows Desktop - WPF
        /// </summary>
        /// <param name="configuration">Configuration to configure</param>
        /// <param name="configureCallback">Callback for the desktop configuration</param>
        /// <returns></returns>
        public static IConfigure Desktop(this IFrontendConfiguration configuration, Action<DesktopConfiguration> configureCallback = null)
        {
            var desktopConfiguration = new DesktopConfiguration();
            configuration.Target = desktopConfiguration;
            if (configureCallback != null) configureCallback(desktopConfiguration);
            return Configure.Instance;
        }
    }
}
