/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Applications;
using Bifrost.Lifecycle;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines the envelope for the event with all the metadata related to the event
    /// </summary>
    public interface IEventEnvelope
    {
        /// <summary>
        /// Gets the <see cref="TransactionCorrelationId"/> that the <see cref="IEvent"/> is part of
        /// </summary>
        TransactionCorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="EventId"/> representing the <see cref="IEvent"/>s
        /// </summary>
        EventId EventId { get; }

        /// <summary>
        /// Gets the global sequence number used in the <see cref="IEventStore"/>
        /// </summary>
        EventSequenceNumber SequenceNumber { get; }

        /// <summary>
        /// Gets the global sequence number for the specific <see cref="IEvent">event type</see>
        /// </summary>
        EventSequenceNumber SequenceNumberForEventType { get; }


        /// <summary>
        /// Gets the <see cref="EventGeneration"/> for the <see cref="IEvent"/>
        /// </summary>
        EventGeneration Generation { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceIdentifier">identifier</see> identifying the <see cref="IEvent"/>
        /// </summary>
        IApplicationResourceIdentifier Event { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceId">id</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceId EventSourceId { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceIdentifier">identifier</see> identifying the <see cref="IEventSource"/>
        /// </summary>
        IApplicationResourceIdentifier EventSource { get; }

        /// <summary>
        /// Gets the <see cref="EventSourceVersion">version</see> of the <see cref="IEventSource"/>
        /// </summary>
        EventSourceVersion Version { get; }

        /// <summary>
        /// Gets who or what the event was caused by.
        /// 
        /// Typically this would be the name of the user or system causing it
        /// </summary>
        CausedBy CausedBy { get; }

        /// <summary>
        /// Gets the time the event occurred
        /// </summary>
        DateTimeOffset Occurred { get; }

        /// <summary>
        /// Creates a new <see cref="EventEnvelope"/> with a different <see cref="TransactionCorrelationId">correlation id</see>
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns>A copy of the <see cref="EventEnvelope"/> with a new <see cref="TransactionCorrelationId"/> </returns>
        IEventEnvelope WithTransactionCorrelationId(TransactionCorrelationId correlationId);

        /// <summary>
        /// Creates a new <see cref="EventEnvelope"/> with a different <see cref="EventSequenceNumber">sequence number</see>
        /// </summary>
        /// <param name="sequenceNumber">The new <see cref="EventSequenceNumber"/></param>
        /// <returns>A copy of the <see cref="EventEnvelope"/> with a new Id </returns>
        IEventEnvelope WithSequenceNumber(EventSequenceNumber sequenceNumber);

        /// <summary>
        /// Creates a new <see cref="EventEnvelope"/> with a different <see cref="EventSequenceNumber">sequence number</see> for the <see cref="IEvent">event type</see>
        /// </summary>
        /// <param name="sequenceNumber">The new <see cref="EventSequenceNumber"/></param>
        /// <returns>A copy of the <see cref="EventEnvelope"/> with a new Id </returns>
        IEventEnvelope WithSequenceNumberForEventType(EventSequenceNumber sequenceNumber);
    }
}