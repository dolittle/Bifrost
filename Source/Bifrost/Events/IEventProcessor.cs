/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Applications;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines something that is capable of processing an event 
    /// </summary>
    public interface IEventProcessor
    {
        /// <summary>
        /// Gets the identifier for the <see cref="IEventProcessor"/>
        /// </summary>
        EventProcessorIdentifier Identifier { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceIdentifier"/> for the <see cref="IEvent">event type</see>
        /// it represents
        /// </summary>
        IApplicationResourceIdentifier Event { get; }

        /// <summary>
        /// Process an event 
        /// </summary>
        /// <param name="envelope"><see cref="IEventEnvelope"/> for <see cref="IEvent"/> to process</param>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        /// <returns><see cref="IEventProcessingResult">Result</see> from the processing</returns>
        IEventProcessingResult Process(IEventEnvelope envelope, IEvent @event);
    }
}