#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Time;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public abstract class Event : IEvent
    {
        static readonly IEnumerable<string> PropertiesToIgnore = typeof(IEvent).GetProperties().Select(p=>p.Name);

#pragma warning disable 1591 // Xml Comments
        public Guid Id { get; set; }

        public string Name { get; set; }
		public string CommandName { get; set; }
        public Guid EventSourceId { get; set; }
        public string AggregatedRoot { get; set; }
    	public string EventSourceName { get; set; }
    	public EventSourceVersion Version { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
		public DateTime Occured { get; set; }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Initializes a new instance of <see cref="Event">Event</see>
        /// </summary>
        protected Event(Guid eventSourceId) : this(eventSourceId,Guid.NewGuid())
        {}

        /// <summary>
        /// Initializes a new instance of <see cref="Event">Event</see> setting the event id directly.  This is required for event versioning.
        /// </summary>
        protected Event(Guid eventSourceId, Guid id)
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

            var properties = type.GetProperties().Where(t=>!PropertiesToIgnore.Contains(t.Name));

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