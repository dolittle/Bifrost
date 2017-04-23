/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Extensions;
#if(!NET461)
using Microsoft.Extensions.Logging;
#endif

namespace Bifrost.Logging
{
    /// <summary>
    /// Represents the entrypoint for configuring logging and the <see cref="ILogAppenders"/>
    /// </summary>
    public class LoggingConfigurator
    {
#if (NET461)
        /// <summary>
        /// Discover any <see cref="ICanConfigureLogAppenders"/> from the entry assembly and setup 
        /// <see cref="ILogAppenders"/>
        /// </summary>
        /// <returns>An instance of <see cref="ILogAppenders"/> that can be used</returns>
        public static ILogAppenders DiscoverAndConfigure()
#else
        /// <summary>
        /// Discover any <see cref="ICanConfigureLogAppenders"/> from the entry assembly and setup 
        /// <see cref="ILogAppenders"/>
        /// </summary>
        /// <returns>An instance of <see cref="ILogAppenders"/> that can be used</returns>
        public static ILogAppenders DiscoverAndConfigure(ILoggerFactory loggerFactory)
#endif
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes();
            var configuratorTypes = types.Where(t => t.HasInterface<ICanConfigureLogAppenders>());

            var configurators = new List<ICanConfigureLogAppenders>();
#if (NET461)
            configurators.Add(new DefaultLogAppendersConfigurator());
#else
            configurators.Add(new DefaultLogAppendersConfigurator(loggerFactory));
#endif
            configurators.AddRange(configuratorTypes.Select(c =>
            {
                if (!c.HasDefaultConstructor()) throw new LogAppenderConfiguratorMissingDefaultConstructor(c);
                return Activator.CreateInstance(c) as ICanConfigureLogAppenders;
            }));

            var logAppenders = new LogAppenders(configurators);
            return logAppenders;
        }
    }
}
