/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Applications;

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
        public IEnumerable<EventAndEnvelope> GetFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public EventSourceVersion GetVersionFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasEventsFor(IApplicationResourceIdentifier eventSource, EventSourceId eventSourceId)
        {
            throw new NotImplementedException();
        }
    }
}
