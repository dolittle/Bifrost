/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanReceiveCommittedEventStream"/> that does nothing
    /// </summary>
    public class NullCommittedEventStreamReceiver : ICanReceiveCommittedEventStream
    {
        /// <inheritdoc/>
        public event CommittedEventStreamReceived Received = (s) => { };
    }
}
