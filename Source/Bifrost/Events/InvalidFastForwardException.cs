/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEventSource">EventSource</see> is stateful 
    /// but there has been an attempt to retrieve it without restoring state by replaying events (fast-forwarding)
    /// </summary>
    public class InvalidFastForwardException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        public InvalidFastForwardException()
        {}

        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public InvalidFastForwardException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidFastForwardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}