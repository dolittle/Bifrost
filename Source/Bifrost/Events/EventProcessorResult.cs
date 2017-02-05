/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingResult"/>
    /// </summary>
    public class EventProcessorResult : IEventProcessingResult
    {
        /// <inheritdoc/>
        public IEventProcessor EventProcessor { get; }

        /// <inheritdoc/>
        public EventProcessingStatus Status { get; }
    }
}
