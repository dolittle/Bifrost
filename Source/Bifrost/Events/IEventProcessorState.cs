/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines the state for an <see cref="IEventProcessor"/>
    /// </summary>
    public interface IEventProcessorState
    {
        /// <summary>
        /// Gets the <see cref="IEventProcessor"/> the state is for
        /// </summary>
        IEventProcessor EventProcessor { get; }

        /// <summary>
        /// Gets the <see cref="EventProcessorStatus">status</see> of the processor
        /// </summary>
        EventProcessorStatus Status { get; }


        /// <summary>
        /// Gets the <see cref="EventSequenceNumber"/> from the global sequence number of the last <see cref="IEvent"/> that was processed 
        /// </summary>
        EventSequenceNumber LastProcessedSequenceNumber { get; }

        /// <summary>
        /// Gets the <see cref="EventSequenceNumber"/> of the last <see cref="IEvent"/> that was processed of the given type
        /// </summary>
        EventSequenceNumber LastProcessedSequenceNumberForEventType { get; }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of the last <see cref="IEvent"/> that was processed
        /// </summary>
        DateTimeOffset LastProcessed { get; }

        /// <summary>
        /// Gets the <see cref="EventProcessingStatus"/> of the last <see cref="IEvent"/> that was processed
        /// </summary>
        EventProcessingStatus LastProcessingStatus { get; }
    }
}