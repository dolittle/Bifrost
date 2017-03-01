/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Commands;
using Bifrost.Execution;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for an enumerable of <see cref="IEvent">Events</see> 
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Populates each <see cref="IEvent">Event</see> with the name of the command that caused the event.
        /// </summary>
        /// <param name="events"><see cref="IEnumerable{EventEnvelopeAndEvent}">Events</see> to be extended</param>
        /// <param name="command"><see cref="ICommand">Command</see> that caused the events to be generated</param>
        public static void MarkEventsWithCommandDetails(this IEnumerable<EventEnvelopeAndEvent> events, ICommand command)
        {
            foreach (var combo in events)
            {
                if (string.IsNullOrEmpty(combo.Event.CommandName))
                    combo.Event.CommandName = command == null ? "[Not available]" : command.GetType().Name;

                combo.Event.CommandContext = command.Id;
            }
        }

        /// <summary>
        /// Populates each <see cref="IEvent">Event</see> with elements from the <see cref="IExecutionContext">ExecutionContext</see>
        /// </summary>
        /// <param name="events"><see cref="IEnumerable{EventEnvelopeAndEvent}">Events</see> to be extended</param>
        /// <param name="executionContext"><see cref="IExecutionContext">Execution Context</see> under which the <see cref="IEvent">events</see> were generated</param>
        public static void ExpandExecutionContext(this IEnumerable<EventEnvelopeAndEvent> events, IExecutionContext executionContext)
        {
            if (executionContext == null)
                return;

            foreach (var combo in events)
            {
                combo.Event.CausedBy = executionContext.Principal.Identity.Name;
                combo.Event.Origin = executionContext.System;
            }
        }
    }
}
