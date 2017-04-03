/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Exception that gets thrown if an <see cref="IEvent"/> has a mismatch with the <see cref="IEventSource"/>
    /// its being used with
    /// </summary>
    public class EventBelongsToOtherEventSource : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventBelongsToOtherEventSource"/>
        /// </summary>
        /// <param name="eventEventSourceId"><see cref="EventSourceId"/> for the event</param>
        /// <param name="otherEventSourceId"><see cref="EventSourceId"/> for the other <see cref="IEventSource"/></param>
        public EventBelongsToOtherEventSource(EventSourceId eventEventSourceId, EventSourceId otherEventSourceId) : base($"EventSource '{eventEventSourceId}' from event mismatches with '{otherEventSourceId}'. Hint: You might be trying append an event to an UncommittedEventStream belonging to a different EventSource") { }
    }
}
