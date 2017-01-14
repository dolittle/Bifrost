/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 namespace Bifrost.Events.InProcess
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamBridge"/>
    /// </summary>
    public class CommittedEventStreamBridge : ICommittedEventStreamBridge
    {
        /// <inheritdoc/>
        public event CommittedEventStreamReceived Received = (s) => { };

        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
            Received(committedEventStream);
        }
    }
}
