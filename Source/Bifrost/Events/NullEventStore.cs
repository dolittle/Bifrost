/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventStore"/>
    /// </summary>
    public class NullEventStore : IEventStore
    {
#pragma warning disable 1591 // Xml Comments
        public CommittedEventStream GetForEventSource(IEventSource eventSource, EventSourceId eventSourceId)
        {
            return new CommittedEventStream(eventSourceId);
        }

        public CommittedEventStream Commit(UncommittedEventStream uncommittedEventStream)
        {
            var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, uncommittedEventStream);
            return committedEventStream;
        }

        public EventSourceVersion GetLastCommittedVersion(IEventSource eventSource, EventSourceId eventSourceId)
        {
            return EventSourceVersion.Zero;
        }

        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            return new IEvent[0];
        }

        public IEnumerable<IEvent> GetAll()
        {
            return new IEvent[0];
        }
#pragma warning restore 1591 // Xml Comments
    }
}
