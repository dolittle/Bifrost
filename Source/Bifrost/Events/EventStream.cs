#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Domain;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a collection of events in the order that they were applied.
    /// </summary>
    public class EventStream : IEnumerable<IEvent>
    {
		/// <summary>
		/// Gets a list of all the events in the stream
		/// </summary>
        protected readonly List<IEvent> Events = new List<IEvent>();

		/// <summary>
		/// Initializes a new <see cref="EventStream">EventStream</see>
		/// </summary>
		/// <param name="eventSourceId">Id of the event source - typically an <see cref="AggregateRoot">AggregatedRoot</see></param>
        public EventStream(Guid eventSourceId)
        {
            EventSourceId = eventSourceId;
        }

        /// <summary>
        /// Gets the Id of the Event Source (Aggregate Root) that this Event Stream relates to.
        /// </summary>
        public Guid EventSourceId { get; private set; }

        /// <summary>
        /// Indicates whether there are any events in the Stream.
        /// </summary>
        public bool HasEvents
        {
            get { return this.Count()  > 0; } 
        }

        /// <summary>
        /// The number of Events in the Stream.
        /// </summary>
        public int Count
        {
            get { return Events.Count; }
        }

        /// <summary>
        /// Get a generic enumerator to iterate over the events
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<IEvent> GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}