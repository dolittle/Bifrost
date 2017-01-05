/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concurrency;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IUncommittedEventStreamCoordinator"/> that commits asynchronously
    /// </summary>
    public class AsynchronousUncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
        UncommittedEventStreamCoordinator _actualCoordinator;
        IScheduler _scheduler;

        /// <summary>
        /// Initializes a new instance of <see cref="AsynchronousUncommittedEventStreamCoordinator"/>
        /// </summary>
        /// <param name="actualCoordinator">The actual <see cref="UncommittedEventStreamCoordinator"/> to be used</param>
        /// <param name="scheduler"><see cref="IScheduler"/> to use for scheduling asynchronous tasks</param>
        public AsynchronousUncommittedEventStreamCoordinator(UncommittedEventStreamCoordinator actualCoordinator, IScheduler scheduler)
        {
            _actualCoordinator = actualCoordinator;
            _scheduler = scheduler;
        }


#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream eventStream)
        {
            _scheduler.Start(() => _actualCoordinator.Commit(eventStream));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
