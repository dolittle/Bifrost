/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    public class UncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        IEventStore _eventStore;
        IEventStoreChangeManager _eventStoreChangeManager;
        IEventSubscriptionManager _eventSubscriptionManager;


        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use for saving the events</param>
        /// <param name="eventStoreChangeManager"><see cref="IEventStoreChangeManager"/> to notify about changes</param>
        /// <param name="eventSubscriptionManager"><see cref="IEventSubscriptionManager"/> to process subscriptions</param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            IEventStoreChangeManager eventStoreChangeManager,
            IEventSubscriptionManager eventSubscriptionManager)
        {
            _eventStore = eventStore;
            _eventStoreChangeManager = eventStoreChangeManager;
            _eventSubscriptionManager = eventSubscriptionManager;
        }


#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream uncommittedEventStream)
        {
            var committedEventStream = _eventStore.Commit(uncommittedEventStream);
            _eventSubscriptionManager.Process(committedEventStream);
            _eventStoreChangeManager.NotifyChanges(_eventStore, committedEventStream);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
