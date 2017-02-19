/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;

namespace Bifrost.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Start configuration the application
        /// </summary>
        /// <param name="configure"><see cref="IConfigure">Configureation</see> to configure</param>
        /// <param name="name">Name of the application</param>
        /// <param name="builderCallback">Callback for building</param>
        /// <returns><see cref="IConfigure">Configuration</see> instance</returns>
        public static IConfigure    Application(this IConfigure configure, ApplicationName name, Func<IApplicationConfigurationBuilder, IApplicationConfigurationBuilder> builderCallback)
        {
            IApplicationConfigurationBuilder applicationConfigurationBuilder = new ApplicationConfigurationBuilder(name);
            applicationConfigurationBuilder = builderCallback(applicationConfigurationBuilder);
            var application = applicationConfigurationBuilder.Build();
            configure.Container.Bind(application);
            return configure;
        }
    }
}
