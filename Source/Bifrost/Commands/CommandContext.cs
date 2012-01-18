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
		private readonly List<IAggregatedRoot> _objectsTracked = new List<IAggregatedRoot>();

		/// <summary>
		/// Initializes a new <see cref="CommandContext">CommandContext</see>
		/// </summary>
		/// <param name="command">The <see cref="ICommand">command</see> the context is for</param>
		/// <param name="executionContext">The <see cref="IExecutionContext"/> for the command</param>
		/// <param name="eventStore">The <see cref="IEventStore">event store</see> that will get all the events and persist them</param>
		public CommandContext(
			ICommand command,
			IExecutionContext executionContext,
			IEventStore eventStore)
		{
			Command = command;
			ExecutionContext = executionContext;
			EventStores = new[] {eventStore};
		}


#pragma warning disable 1591 // Xml Comments
		public ICommand Command { get; private set; }
		public IExecutionContext ExecutionContext { get; private set; }

		public IEnumerable<IEventStore> EventStores { get; private set; }

		public void RegisterForTracking(IAggregatedRoot aggregatedRoot)
		{
			_objectsTracked.Add(aggregatedRoot);
		}

		public IEnumerable<IAggregatedRoot> GetObjectsBeingTracked()
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
					events.MarkEventsWithCommand(Command);
                    events.ExpandExecutionContext(ExecutionContext);
                    foreach (var eventStore in EventStores)
                        eventStore.Save(events);

                    trackedObject.Commit();
				}
			}
		}

		public void Rollback()
		{
			// Todo : Should rollback any aggregated roots that are being tracked - 
			// PS: What do you do with events that has already been dispatched and stored?
		}
#pragma warning restore 1591 // Xml Comments
	}
}
