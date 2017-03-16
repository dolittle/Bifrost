/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessorLog"/>
    /// </summary>
    public class EventProcessorLog : IEventProcessorLog
    {
        /// <inheritdoc/>
        public void Failed(IEventProcessor processor, IEvent @event, IEventEnvelope envelope)
        {
            
        }
    }
}
