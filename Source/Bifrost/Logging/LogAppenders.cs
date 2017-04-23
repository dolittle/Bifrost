/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogAppenders"/>
    /// </summary>
    [Singleton]
    public class LogAppenders : ILogAppenders
    {
        List<ILogAppender> _appenders = new List<ILogAppender>();

        /// <summary>
        /// Initializes a new instance of <see cref="LogAppenders"/>
        /// </summary>
        /// <param name="logAppendersConfigurators"><see cref="IEnumerable{T}">Instances of <see cref="ICanConfigureLogAppenders"/></see></param>
        public LogAppenders(IEnumerable<ICanConfigureLogAppenders> logAppendersConfigurators)
        {
            logAppendersConfigurators.ForEach(l => l.Configure(this));
        }

        /// <inheritdoc/>
        public IEnumerable<ILogAppender> Appenders => _appenders;

        /// <inheritdoc/>
        public void Add(ILogAppender appender)
        {
            _appenders.Add(appender);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _appenders.Clear();
        }

        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            _appenders.ForEach(l =>
            {
                try
                {
                    l.Append(filePath, lineNumber, member, level, message, exception);
                }
                catch
                {
                    // Ignore any errors from any of the appenders - we don't care
                }
            });
        }
    }
}
