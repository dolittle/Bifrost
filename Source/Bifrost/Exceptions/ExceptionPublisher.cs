/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;

namespace Bifrost.Exceptions
{
    /// <summary>
    /// Publishes exceptions to all <see cref="IExceptionSubscriber"/>s.
    /// </summary>
    public class ExceptionPublisher : IExceptionPublisher
    {
        readonly IInstancesOf<IExceptionSubscriber> _subscribers;

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionPublisher"/>.
        /// </summary>
        /// <param name="subscribers">All known subscribers.</param>
        public ExceptionPublisher(IInstancesOf<IExceptionSubscriber> subscribers)
        {
            _subscribers = subscribers;
        }

        /// <summary>
        /// Publishes the exception to all <see cref="IExceptionSubscriber"/>.
        /// </summary>
        /// <param name="exception"></param>
        public void Publish(Exception exception)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Handle(exception);
            }
        }
    }
}