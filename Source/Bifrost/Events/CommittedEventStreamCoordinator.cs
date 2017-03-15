/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamCoordinator"/>
    /// </summary>
    [Singleton]
    public class CommittedEventStreamCoordinator : ICommittedEventStreamCoordinator
    {
        ICanReceiveCommittedEventStream _committedEventStreamReceiver;
        IEventProcessors _eventProcessors;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="committedEventStreamReceiver"><see cref="ICanReceiveCommittedEventStream">Committed event stream receiver</see> for receiving events</param>
        /// <param name="eventProcessors"></param>
        /// <param name="eventProcessorLog"></param>
        public CommittedEventStreamCoordinator(
            ICanReceiveCommittedEventStream committedEventStreamReceiver,
            IEventProcessors eventProcessors,
            IEventProcessorLog eventProcessorLog)
        {
            _committedEventStreamReceiver = committedEventStreamReceiver;
            _eventProcessors = eventProcessors;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _committedEventStreamReceiver.Received += CommittedEventStreamReceived;
        }

        void CommittedEventStreamReceived(CommittedEventStream committedEventStream)
        {
            //_eventSubscriptionManager.Process(committedEventStream);

            committedEventStream.ForEach(e =>
            {
                var result = _eventProcessors.Process(e.Event);
                
            });
        }
    }
}
