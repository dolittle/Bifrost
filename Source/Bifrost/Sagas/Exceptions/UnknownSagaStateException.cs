/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas.Exceptions
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="ISaga">Saga</see> is in an unknown <see cref="SagaState">State</see>
    /// </summary>
    public class UnknownSagaStateException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="UnknownSagaStateException">UnknownSagaStateException</see>
        /// </summary>
        public UnknownSagaStateException()
            : base()
        {
        }

        /// <summary>
        /// Initializes an <see cref="UnknownSagaStateException">UnknownSagaStateException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        public UnknownSagaStateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an <see cref="UnknownSagaStateException">UnknownSagaStateException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner exception</param>
        public UnknownSagaStateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}