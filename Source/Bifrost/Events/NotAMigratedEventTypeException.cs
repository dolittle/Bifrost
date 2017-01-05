/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEvent">Event</see> in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has not implemented the correct <see cref="Bifrost.Events.IAmNextGenerationOf{T}">interface</see>.
    /// </summary>
    public class NotAMigratedEventTypeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        public NotAMigratedEventTypeException()
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public NotAMigratedEventTypeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public NotAMigratedEventTypeException(string message, Exception innerException) : base(message,innerException)
        { }
    }
}