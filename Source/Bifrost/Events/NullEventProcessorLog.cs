/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventProcessorLog"/>
    /// </summary>

    public class NullEventProcessorLog : IEventProcessorLog
    {
        /// <inheritdoc/>
        public void Failed(IEventProcessor processor, IEvent @event, IEventEnvelope envelope)
        {
        }
    }
}
