/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Messaging
{
    /// <summary>
    /// Defines a messenger that provides a publish / subscribe bus
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Publish a message of a given type
        /// </summary>
        /// <typeparam name="T">Type of message to publish</typeparam>
        /// <param name="content">Message to publish</param>
        void Publish<T>(T content);

        /// <summary>
        /// Subscribe to a given message by its type
        /// </summary>
        /// <typeparam name="T">Type to subscribe to</typeparam>
        /// <param name="receivedCallback"><see cref="Action{T}"/> that gets called when a message is received</param>
        void SubscribeTo<T>(Action<T> receivedCallback);
    }
}
