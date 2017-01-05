/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas.Exceptions
{
    /// <summary>
    /// Exception indicating that the transition between two <see cref="SagaState">SagaStates</see> is invalid.
    /// </summary>
    public class InvalidSagaStateTransitionException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        public InvalidSagaStateTransitionException()
            : base()
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        public InvalidSagaStateTransitionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidSagaStateTransitionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}