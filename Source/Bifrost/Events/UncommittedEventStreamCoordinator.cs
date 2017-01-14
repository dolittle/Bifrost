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
        ICommittedEventStreamCoordinator _committedEventStreamCoordinator;

        /// <summary>
        /// Initializes an instance of a <see cref="UncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use for saving the events</param>
        /// <param name="committedEventStreamCoordinator"><see cref="ICommittedEventStreamCoordinator"/> coordinate the <see cref="CommittedEventStream"/></param>
        public UncommittedEventStreamCoordinator(
            IEventStore eventStore,
            ICommittedEventStreamCoordinator committedEventStreamCoordinator)
        {
            _eventStore = eventStore;
            _committedEventStreamCoordinator = committedEventStreamCoordinator;
        }


#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream uncommittedEventStream)
        {
            var committedEventStream = _eventStore.Commit(uncommittedEventStream);
            _committedEventStreamCoordinator.Handle(committedEventStream);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
