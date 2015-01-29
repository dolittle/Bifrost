#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using Bifrost.Domain;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a stream of events that are uncommitted
    /// </summary>
    public class UncommittedEventStream : EventStream
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UncommittedEventStream">UncommittedEventStream</see>
        /// </summary>
        /// <param name="eventSourceId">Id of the event source - typically an <see cref="AggregateRoot">AggregatedRoot</see></param>
        public UncommittedEventStream(Guid eventSourceId)
            : base(eventSourceId)
        {

        }

        /// <summary>
        /// Appends an event to the uncommitted event stream, setting the correct EventSourceId and Sequence Number for the event.
        /// </summary>
        /// <param name="event">The event to be appended.</param>
        public void Append(IEvent @event)
        {
            EnsureEventCanBeAppendedToThisEventSource(@event);
            AttachAndSequenceEvent(@event);

            Events.Add(@event);
        }

        private void AttachAndSequenceEvent(IEvent @event)
        {
            @event.EventSourceId = EventSourceId;
        }

        private void EnsureEventCanBeAppendedToThisEventSource(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("Cannot append a null event");
        }
    }
}