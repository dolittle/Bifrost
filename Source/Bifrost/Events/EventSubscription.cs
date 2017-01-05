/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a subscription for a specific <see cref="IProcessEvents"/> and method on it that can receive a method
    /// </summary>
    public class EventSubscription
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="EventSubscription"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the owner of the subscriber method that subscribes to the event
        /// </summary>
        public Type Owner { get; set; }

        /// <summary>
        /// Gets or sets the method that is subscribing to the event
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets the actual event type that the subscriber handles
        /// </summary>
        public Type EventType { get; set; }

        /// <summary>
        /// Gets or sets the actual event name that the subscriber handles
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the last event id the subscriber has processed
        /// </summary>
        public long LastEventId { get; set; }


        /// <summary>
        /// Check wether or not subscription should process the event
        /// </summary>
        /// <param name="event">Event to check</param>
        /// <returns>True if it should process, false if not</returns>
        public bool ShouldProcess(IEvent @event)
        {
            return @event.Id > LastEventId;
        }


#pragma warning disable 1591 // Xml Comments
        public override bool Equals(object obj)
        {
            var otherSubscription = obj as EventSubscription;

            if (otherSubscription == null)
                return false;

            return Owner.Equals(otherSubscription.Owner) &&
                Method.Equals(otherSubscription.Method) &&
                EventName.Equals(otherSubscription.EventName);
        }

        public override int GetHashCode()
        {
            return string.Format("{0} - {1} - {2}", Owner.Name, Method.Name, EventName).GetHashCode();
        }
#pragma warning restore 1591 // Xml Comments

    }
}
