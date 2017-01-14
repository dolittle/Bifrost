/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamCoordinator"/>
    /// </summary>
    public class CommittedEventStreamCoordinator : ICommittedEventStreamCoordinator
    {
        ICanSendCommittedEventStream _committedEventStreamSender;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        public CommittedEventStreamCoordinator(ICanSendCommittedEventStream committedEventStreamSender)
        {
            _committedEventStreamSender = committedEventStreamSender;
        }

        /// <inheritdoc/>
        public void Handle(CommittedEventStream committedEventStream)
        {
            _committedEventStreamSender.Send(committedEventStream);
        }
    }
}
