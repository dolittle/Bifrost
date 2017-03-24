/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Events.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStore"/> for MongoDB
    /// </summary>
    public class EventStore : IEventStore
    {
        /// <inheritdoc/>
        public void Commit(IEnumerable<EventAndEnvelope> eventsAndEnvelopes)
        {
            
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public CommittedEventStream GetFor(IEventSource eventSource)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public EventSourceVersion GetLastCommittedVersionFor(IEventSource eventSource)
        {
            throw new NotImplementedException();
        }
    }
}
