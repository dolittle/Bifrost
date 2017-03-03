/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventStore"/>
    /// </summary>
    public class NullEventStore : IEventStore
    {
        /// <inheritdoc/>
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            return new CommittedEventStream(eventSource.EventSourceId);
        }

        /// <inheritdoc/>
        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
            return committedEventStream;
        }

        /// <inheritdoc/>
        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            return EventSourceVersion.Zero;
        }
    }
}
