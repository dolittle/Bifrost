/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system for logging related to <see cref="IEventProcessor">event processors</see>
    /// </summary>
    public interface IEventProcessorLog
    {
        /// <summary>
        /// Report information coming from an <see cref="IEventProcessor"/> with the related <see cref="IEvent"/>
        /// </summary>
        /// <param name="processor"><see cref="IEventProcessor"/> to report for</param>
        /// <param name="event"><see cref="IEvent"/> that was being processed</param>
        /// <param name="envelope"><see cref="IEventEnvelope"/> related to the <see cref="IEvent"/> being processed</param>
        /// <param name="messages"><see cref="IEnumerable{EventProcessingMessage}">Messages</see> related to the failed processing</param>
        void Info(IEventProcessor processor, IEvent @event, IEventEnvelope envelope, IEnumerable<EventProcessingMessage> messages);

        /// <summary>
        /// Report failure coming from an <see cref="IEventProcessor"/> with the related <see cref="IEvent"/>
        /// </summary>
        /// <param name="processor"><see cref="IEventProcessor"/> to report for</param>
        /// <param name="event"><see cref="IEvent"/> that was being processed</param>
        /// <param name="envelope"><see cref="IEventEnvelope"/> related to the <see cref="IEvent"/> being processed</param>
        /// <param name="messages"><see cref="IEnumerable{EventProcessingMessage}">Messages</see> related to the failed processing</param>
        void Failed(IEventProcessor processor, IEvent @event, IEventEnvelope envelope, IEnumerable<EventProcessingMessage> messages);
    }
}