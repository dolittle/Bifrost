/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
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
        IEventSubscriptionManager _eventSubscriptionManager;
        IEventProcessors _eventProcessors;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="committedEventStreamReceiver"><see cref="ICanReceiveCommittedEventStream">Committed event stream receiver</see> for receiving events</param>
        /// <param name="eventSubscriptionManager"><see cref="IEventSubscriptionManager"/> for handling processing of <see cref="IEvent">events</see></param>
        /// <param name="eventProcessors"></param>
        public CommittedEventStreamCoordinator(
            ICanReceiveCommittedEventStream committedEventStreamReceiver, 
            IEventSubscriptionManager eventSubscriptionManager,
            IEventProcessors eventProcessors)
        {
            _committedEventStreamReceiver = committedEventStreamReceiver;
            _eventSubscriptionManager = eventSubscriptionManager;
            _eventProcessors = eventProcessors;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _committedEventStreamReceiver.Received += CommittedEventStreamReceived;
        }

        void CommittedEventStreamReceived(CommittedEventStream committedEventStream)
        {
            _eventSubscriptionManager.Process(committedEventStream.Select(e=>e.Event));

            committedEventStream.ForEach(e => _eventProcessors.Process(e.Event));
        }
    }
}
