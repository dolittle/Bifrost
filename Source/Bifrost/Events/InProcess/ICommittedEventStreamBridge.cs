/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.InProcess
{
    /// <summary>
    /// Defines a bridge between <see cref="CommittedEventStreamSender"/> and <see cref="CommittedEventStreamReceiver"/>
    /// </summary>
    public interface ICommittedEventStreamBridge
    {
        /// <summary>
        /// Event that is triggered when a <see cref="CommittedEventStream"/> is received
        /// </summary>
        event CommittedEventStreamReceived Received;

        /// <summary>
        /// Send a <see cref="CommittedEventStream"/>
        /// </summary>
        void Send(CommittedEventStream committedEventStream);
    }
}