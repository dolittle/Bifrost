/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
#if(!NET461)
using Microsoft.Extensions.Logging;
#endif

namespace Bifrost.Logging
{
    /// <summary>
    /// Represents the default <see cref="ICanConfigureLogAppenders">configurator</see> for <see cref="ILogAppenders"/>
    /// </summary>
    public class DefaultLogAppendersConfigurator : ICanConfigureLogAppenders
    {
#if(!NET461)
        ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppendersConfigurator"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        public DefaultLogAppendersConfigurator(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
#endif


        /// <inheritdoc/>
        public void Configure(ILogAppenders appenders)
        {
#if (NET461)
            var defaultLogAppender = new DefaultLogAppender();
#else
            var defaultLogAppender = new DefaultLogAppender(_loggerFactory);
#endif
            appenders.Add(defaultLogAppender);
        }
    }
}
