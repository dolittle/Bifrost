#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;
using System.Security.Principal;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Execution;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandContext">ICommandContext</see>
	/// </summary>
	public class CommandContext : ICommandContext
	{
        IEventStore _eventStore;
        IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
		List<IAggregateRoot> _objectsTracked = new List<IAggregateRoot>();

		/// <summary>
		/// Initializes a new <see cref="CommandContext">CommandContext</see>
		/// </summary>
		/// <param name="command">The <see cref="ICommand">command</see> the context is for</param>
		/// <param name="executionContext">The <see cref="IExecutionContext"/> for the command</param>
        /// <param name="eventStore">A <see cref="IEventStore"/> that will receive any events generated</param>
        /// <param name="uncommittedEventStreamCoordinator">The <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinating the committing of events</param>
		public CommandContext(
			ICommand command,
			IExecutionContext executionContext,
            IEventStore eventStore,
			IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator)
		{
			Command = command;
			ExecutionContext = executionContext;
            _eventStore = eventStore;
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
		}


#pragma warning disable 1591 // Xml Comments
		public ICommand Command { get; private set; }
		public IExecutionContext ExecutionContext { get; private set; }

		public void RegisterForTracking(IAggregateRoot aggregatedRoot)
		{
			_objectsTracked.Add(aggregatedRoot);
		}

		public IEnumerable<IAggregateRoot> GetObjectsBeingTracked()
		{
			return _objectsTracked;
		}


		/// <summary>
		/// Disposes the CommandContext by Committing
		/// </summary>
		public void Dispose()
		{
			Commit();
		}

		public void Commit()
		{
			var trackedObjects = GetObjectsBeingTracked();
			foreach (var trackedObject in trackedObjects)
			{
				var events = trackedObject.UncommittedEvents;
				if (events.HasEvents)
				{
					events.MarkEventsWithCommandDetails(Command);
                    events.ExpandExecutionContext(ExecutionContext);
                    _uncommittedEventStreamCoordinator.Commit(events);
                    trackedObject.Commit();
				}
			}
		}

		public void Rollback()
		{
			// Todo : Should rollback any aggregated roots that are being tracked - 
			// PS: What do you do with events that has already been dispatched and stored?
		}


        public CommittedEventStream GetCommittedEventsFor(EventSource eventSource, Guid eventSourceId)
        {
            return _eventStore.GetForEventSource(eventSource, eventSourceId);
        }

        public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
        {
            return _eventStore.GetLastCommittedVersion(eventSource, eventSourceId);
        }

#pragma warning restore 1591 // Xml Comments


    }
}
