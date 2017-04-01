/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public abstract class Event : IEvent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Event">Event</see>
        /// </summary>
        protected Event(EventSourceId eventSourceId)
        {
            EventSourceId = eventSourceId;
        }

        /// <inheritdoc/>
        public EventSourceId EventSourceId { get; set; }

        /// <summary>
        /// Compares the event with another event, but will skip properties that are on the <see cref="IEvent"/> interface
        /// </summary>
        /// <param name="obj">The other event to compare to</param>
        /// <returns>True if equal, false if not</returns>
        /// <remarks>
        /// Passing in an event of a different type automatically result in false
        /// </remarks>
        public override bool Equals(object obj)
        {
            var type = GetType();
            if (obj == null || obj.GetType() != type )
                return false;

            foreach( var property in type.GetTypeInfo().DeclaredProperties)
            {
                var value = property.GetValue(this, null);
                var otherValue = property.GetValue(obj, null);
                if (value != otherValue)
                    return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}