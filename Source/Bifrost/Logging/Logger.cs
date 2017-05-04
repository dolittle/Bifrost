/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;

namespace Bifrost.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogger"/>
    /// </summary>
    [Singleton]
    public class Logger : ILogger
    {
        static ILogger _internal;

        /// <summary>
        /// Internal logger for those scenarios where it can't be or it is inconvenient to get it injected
        /// </summary>
        internal static ILogger Internal => _internal;

        static Logger()
        {
            _internal = new NullLogger();
        }

        ILogAppenders _logAppenders;

        /// <summary>
        /// Initializes a new instance of <see cref="Logger"/>
        /// </summary>
        /// <param name="logAppenders">The <see cref="ILogAppenders">log appenders</see></param>
        public Logger(ILogAppenders logAppenders)
        {
            _logAppenders = logAppenders;
            _internal = this;
        }

        /// <inheritdoc/>
        public void Trace(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Trace, message);
        }

        /// <inheritdoc/>
        public void Debug(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Debug, message);
        }

        /// <inheritdoc/>
        public void Information(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Info, message);
        }

        /// <inheritdoc/>
        public void Warning(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Warning, message);
        }

        /// <inheritdoc/>
        public void Critical(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Critical, message);
        }

        /// <inheritdoc/>
        public void Critical(Exception exception, string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Critical, message, exception);
        }

        /// <inheritdoc/>
        public void Error(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Error, message);
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Error, message, exception);
        }
    }
}
