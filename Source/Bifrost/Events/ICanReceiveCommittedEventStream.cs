/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines something that is capable of receiving <see cref="CommittedEventStream"/>
    /// </summary>
    public interface ICanReceiveCommittedEventStream
    {
        /// <summary>
        /// Event that fires when a new <see cref="CommittedEventStream"/> is received
        /// </summary>
        event CommittedEventStreamReceived Received;
    }
}
