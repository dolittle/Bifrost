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
        /// Gets the <see cref="EventId"/> of the last <see cref="IEvent"/> that was processed
        /// </summary>
        EventId LastEvent { get; }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of the last <see cref="IEvent"/> that was processed
        /// </summary>
        DateTimeOffset LastProcessed { get; }

        /// <summary>
        /// Gets the <see cref="EventProcessingStatus"/> of the last <see cref="IEvent"/> that was processed
        /// </summary>
        EventProcessingStatus LastStatus { get; }
    }
}