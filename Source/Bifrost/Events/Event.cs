/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Time;
using System.Reflection;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public abstract class Event : IEvent
    {
        static readonly IEnumerable<string> PropertiesToIgnore = typeof(IEvent).GetTypeInfo().DeclaredProperties.Select(p => p.Name);

#pragma warning disable 1591 // Xml Comments
        public long Id { get; set; }
        public Guid CommandContext { get; set; }
        public string Name { get; set; }
		public string CommandName { get; set; }
        public Guid EventSourceId { get; set; }
    	public string EventSource { get; set; }
    	public EventSourceVersion Version { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
		public DateTime Occured { get; set; }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Initializes a new instance of <see cref="Event">Event</see>
        /// </summary>
        protected Event(Guid eventSourceId) : this(eventSourceId,0)
        {}

        /// <summary>
        /// Initializes a new instance of <see cref="Event">Event</see> setting the event id directly.  This is required for event versioning.
        /// </summary>
        protected Event(Guid eventSourceId, long id)
        {
            Id = id;
            EventSourceId = eventSourceId;
            Name = GetType().Name;
        	Occured = SystemClock.GetCurrentTime();
        }

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

            var properties = type.GetTypeInfo().DeclaredProperties.Where(t => !PropertiesToIgnore.Contains(t.Name));
            foreach( var property in properties )
            {
                var value = property.GetValue(this, null);
                var otherValue = property.GetValue(obj, null);
                if (value != otherValue)
                    return false;
            }

            return true;
        }

#pragma warning disable 1591 // Xml Comments
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
#pragma warning restore 1591 // Xml Comments
	}
}