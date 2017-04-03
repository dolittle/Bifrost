/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessorState"/>
    /// </summary>
    public class EventProcessorState : IEventProcessorState
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessorState"/>
        /// </summary>
        /// <param name="eventProcessor"><see cref="IEventProcessor"/> the state is for</param>
        /// <param name="status"><see cref="EventProcessorStatus">Status</see> of the <see cref="IEventProcessor"/></param>
        /// <param name="lastProcessed">When processed last</param>
        /// <param name="lastProcessedSequenceNumber">Last <see cref="EventSequenceNumber"/></param>
        /// <param name="lastProcessedSequenceNumberForEventType">Last <see cref="EventSequenceNumber"/> for the <see cref="IEvent">event type</see></param>
        /// <param name="lastProcessingStatus"><see cref="EventProcessingStatus"/> of the processing</param>
        public EventProcessorState(
            IEventProcessor eventProcessor,
            EventProcessorStatus status,
            DateTimeOffset lastProcessed,
            EventSequenceNumber lastProcessedSequenceNumber,
            EventSequenceNumber lastProcessedSequenceNumberForEventType,
            EventProcessingStatus lastProcessingStatus)
        {
            Status = status;
            EventProcessor = eventProcessor;
            LastProcessed = lastProcessed;
            LastProcessedSequenceNumber = lastProcessedSequenceNumber;
            LastProcessedSequenceNumberForEventType = lastProcessedSequenceNumberForEventType;
            LastProcessingStatus = lastProcessingStatus;
        }

        /// <inheritdoc/>
        public IEventProcessor EventProcessor { get;  }

        /// <inheritdoc/>
        public EventProcessorStatus Status { get; }

        /// <inheritdoc/>
        public DateTimeOffset LastProcessed { get; }

        /// <inheritdoc/>
        public EventSequenceNumber LastProcessedSequenceNumber { get; }

        /// <inheritdoc/>
        public EventSequenceNumber LastProcessedSequenceNumberForEventType { get; }

        /// <inheritdoc/>
        public EventProcessingStatus LastProcessingStatus { get; }

    }
}
