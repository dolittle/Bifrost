/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommittedEventStreamFactory"/>
    /// </summary>
    public class CommittedEventStreamFactory : ICommittedEventStreamFactory
    {
        IEventEnvelopes _eventEnvelopes;

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamFactory"/>
        /// </summary>
        /// <param name="eventEnvelopes"><see cref="IEventEnvelopes"/> for creating <see cref="IEventEnvelope"/></param>
        public CommittedEventStreamFactory(IEventEnvelopes eventEnvelopes)
        {
            _eventEnvelopes = eventEnvelopes;
        }


        /// <inheritdoc/>
        public CommittedEventStream CreateFrom(UncommittedEventStream eventStream)
        {
            throw new NotImplementedException();
        }
    }
}
