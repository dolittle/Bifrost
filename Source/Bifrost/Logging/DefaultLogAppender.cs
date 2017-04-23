/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
#if(NET461)
using System.Diagnostics;
#else
using Microsoft.Extensions.Logging;
#endif

namespace Bifrost.Logging
{
#if (NET461)
    /// <summary>
    /// Represents a default implementation of <see cref="ILogAppender"/> for using System.Diagnostics.Debug
    /// </summary>
#else
    /// <summary>
    /// Represents a default implementation of <see cref="ILogAppender"/> for using ILogger
    /// </summary>
#endif
    public class DefaultLogAppender : ILogAppender
    {
#if (NET461)
        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            Debug.Write($"[{level}] - {message}", $"{filePath}[{lineNumber}] - {member}");
        }
#else
        ILoggerFactory _loggerFactory;
        Dictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = new Dictionary<string, Microsoft.Extensions.Logging.ILogger>();

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppender"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        public DefaultLogAppender(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            Microsoft.Extensions.Logging.ILogger logger;
            var loggerKey = filePath;
            if (!_loggers.ContainsKey(loggerKey))
            {
                logger = _loggerFactory.CreateLogger(loggerKey);
                _loggers[loggerKey] = logger;
            }
            else logger = _loggers[loggerKey];

            switch( level )
            {
                case LogLevel.Trace: logger.LogTrace(message); break;
                case LogLevel.Debug: logger.LogDebug(message); break;
                case LogLevel.Info: logger.LogInformation(message); break;
                case LogLevel.Warning: logger.LogWarning(message); break;
                case LogLevel.Critical: logger.LogCritical(message); break;
                case LogLevel.Error: logger.LogError(message); break;
            }
        }
#endif
    }
}
