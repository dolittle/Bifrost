/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEvent">Event</see> in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has not been registered as an <see cref="IEvent">Event</see>.
    /// </summary>
    public class UnregisteredEventException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        public UnregisteredEventException()
        {}

        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        /// <param name="message">Error Message</param>
        public UnregisteredEventException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public UnregisteredEventException(string message, Exception innerException) : base(message,innerException)
        { }
    }
}