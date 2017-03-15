/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a message that can occur during <see cref="IEventProcessor">event processing</see>
    /// </summary>
    public class EventProcessingMessage : Value<EventProcessingMessage>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessingMessage"/>
        /// </summary>
        /// <param name="severity"><see cref="EventProcessingMessageSeverity"/> for the message</param>
        /// <param name="message"><see cref="string"/> representing the message</param>
        public EventProcessingMessage(EventProcessingMessageSeverity severity, string message)
        {
            Severity = severity;
            Message = message;
            Details = new string[0];
        }


        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessingMessage"/>
        /// </summary>
        /// <param name="severity"><see cref="EventProcessingMessageSeverity"/> for the message</param>
        /// <param name="message"><see cref="string"/> representing the message</param>
        /// <param name="details"><see cref="IEnumerable{T}">Strings</see> of details</param>
        public EventProcessingMessage(EventProcessingMessageSeverity severity, string message, IEnumerable<string> details)
        {
            Severity = severity;
            Message = message;
            Details = details;
        }

        /// <summary>
        /// Gets the severity of the <see cref="EventProcessingMessage"/>
        /// </summary>
        public EventProcessingMessageSeverity   Severity { get; }

        /// <summary>
        /// Gets the actual <see cref="string"/> for the <see cref="EventProcessingMessage"/>
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the details for the <see cref="EventProcessingMessage"/>
        /// </summary>
        public IEnumerable<string> Details { get; }
    }
}