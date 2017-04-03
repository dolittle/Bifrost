/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.InProcess
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanSendCommittedEventStream"/> for in-process purpose
    /// </summary>
    public class CommittedEventStreamSender : ICanSendCommittedEventStream
    {
        ICommittedEventStreamBridge _bridge;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamSender"/>
        /// </summary>
        public CommittedEventStreamSender(ICommittedEventStreamBridge bridge)
        {
            _bridge = bridge;
        }


        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
            _bridge.Send(committedEventStream);
        }
    }
}
