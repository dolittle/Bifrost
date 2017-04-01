/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingResult"/>
    /// </summary>
    public class EventProcessingResult : IEventProcessingResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessingResult"/>
        /// </summary>
        /// <param name="correlationId"><see cref="TransactionCorrelationId"/> the result is related to</param>
        /// <param name="eventProcessor"><see cref="IEventProcessor"/> the result is from</param>
        /// <param name="status"><see cref="EventProcessingStatus">Status</see> of the processing</param>
        /// <param name="start"><see cref="DateTimeOffset">Start time</see> of processing</param>
        /// <param name="end"><see cref="DateTimeOffset">End time</see> of processing</param>
        /// <param name="messages"><see cref="EventProcessingMessage">Messages</see> that occured during processing</param>
        public EventProcessingResult(
            TransactionCorrelationId correlationId,
            IEventProcessor eventProcessor, 
            EventProcessingStatus status, 
            DateTimeOffset start,
            DateTimeOffset end,
            IEnumerable<EventProcessingMessage> messages)
        {
            CorrelationId = correlationId;
            EventProcessor = eventProcessor;
            Status = status;
            Start = start;
            End = end;
            Messages = messages;
        }

        /// <inheritdoc/>
        public TransactionCorrelationId CorrelationId { get; }

        /// <inheritdoc/>
        public IEventProcessor EventProcessor { get; }

        /// <inheritdoc/>
        public EventProcessingStatus Status { get; }

        /// <inheritdoc/>
        public DateTimeOffset Start { get; }

        /// <inheritdoc/>
        public DateTimeOffset End { get; }

        /// <inheritdoc/>
        public IEnumerable<EventProcessingMessage> Messages { get; }
    }
}
