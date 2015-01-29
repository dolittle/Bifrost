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
using System.Linq.Expressions;

namespace Bifrost.Events
{
	/// <summary>
	/// An EventSource is a domain object that is capable of generating and applying events.  It is an AggregateRoot in the context
	/// of event sourcing.
	/// </summary>
	public interface IEventSource
    {
		/// <summary>
		/// The Id of the Event Source.  
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Gets the version of this EventSource
		/// </summary>
		EventSourceVersion Version { get; }

		/// <summary>
		/// A stream of events that have been applied to the <seealso cref="EventSource">EventSource</seealso> but have not yet been committed to the EventStore.
		/// </summary>
		UncommittedEventStream UncommittedEvents { get; }

		/// <summary>
		/// Apply a new event to the EventSource.  This will be applied and added to the <see cref="UncommittedEvents">UncommitedEvents</see>.
		/// </summary>
		/// <param name="event">The event that is to be applied</param>
    	void Apply(IEvent @event);

		/// <summary>
		/// Apply a new event based upon a method to the EventSource. This will applied and added to the <see cref="UncommittedEvents">UncommitedEvents</see>
		/// </summary>
		/// <param name="expression">Expression pointing to a method to use for applying the event</param>
    	void Apply(Expression<Action> expression);

		/// <summary>
		/// Reapply an event from a stream
		/// </summary>
		/// <param name="eventStream">Stream that contains the events to reapply</param>
    	void ReApply(CommittedEventStream eventStream);

        /// <summary>
        /// Fast forward to the specified version of the <seealso cref="EventSource">EventSource</seealso>
        /// </summary>
        /// <param name="lastVersion">Version to fast foward to</param>
        void FastForward(EventSourceVersion lastVersion);
    }
}