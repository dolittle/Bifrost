/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.CompilerServices;

namespace Bifrost.Logging
{
    /// <summary>
    /// Defines a logging system
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Output a trace message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Trace(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output a debug message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Debug(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output an informational message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Information(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output an warning message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Warning(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output a critical message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Critical(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output a critical message
        /// </summary>
        /// <param name="exception">Related exception to the message</param>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Critical(
            Exception exception,
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output an error message
        /// </summary>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Error(
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");

        /// <summary>
        /// Output an informational message
        /// </summary>
        /// <param name="exception">Related exception to the message</param>
        /// <param name="message">Message to output</param>
        /// <param name="filePath">FilePath of the caller</param>
        /// <param name="lineNumber">Linenumber in the file of the caller</param>
        /// <param name="member">Membername of the caller</param>
        void Error(
            Exception exception,
            string message,
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0,
            [CallerMemberName]string member = "");
    }
}
