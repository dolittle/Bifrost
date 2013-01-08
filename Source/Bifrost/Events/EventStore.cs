#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System.Linq;
using Bifrost.Execution;
using Bifrost.Globalization;
using Bifrost.Entities;
using System.Collections.Generic;

namespace Bifrost.Events
{
	/// <summary>
	/// Represents an <see cref="IEventStore"/>
	/// </summary>
    public class EventStore : IEventStore
    {
        IEntityContext<IEvent> _entityContext;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

	    /// <summary>
	    /// Initializes a new instance of <see cref="EventStore"/>
	    /// </summary>
	    /// <param name="entityContext"><see cref="IEntityContext{IEvent}"/> that persists events</param>
        /// <param name="eventMigrationHierarchyManager"><see cref="IEventMigrationHierarchyManager"/> for dealing with migration hierarchies for events</param>
	    public EventStore(
            IEntityContext<IEvent> entityContext,
            IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _entityContext = entityContext;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

#pragma warning disable 1591 // Xml Comments
        public CommittedEventStream GetForEventSource(EventSource eventSource, Guid eventSourceId)
        {
            var eventSourceType = eventSource.GetType();

            var events = _entityContext
                            .Entities
                                .Where(
                                    e => e.EventSourceId == eventSourceId && 
                                         e.EventSource == eventSourceType.AssemblyQualifiedName
                                    ).ToArray();

            var stream = new CommittedEventStream(eventSourceId);
            stream.Append(events);
            return stream;
        }

        public void Commit(UncommittedEventStream events)
        {
            var eventArray = events.ToArray();
            for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
            {
                var @event = eventArray[eventIndex];
                _entityContext.Insert(@event);
            }

            _entityContext.Commit();
        }

	    public EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId)
	    {
            var @event = _entityContext.Entities
                .Where(e => e.EventSourceId == eventSourceId)
                    .OrderByDescending(e => e.Version)
                .FirstOrDefault();

            if (@event == null)
                return EventSourceVersion.Zero;

            return @event.Version;
	    }


        public IEnumerable<IEvent> GetBatch(int batchesToSkip, int batchSize)
        {
            var events = _entityContext.Entities.Skip(batchSize * batchesToSkip).Take(batchSize);
            return events.ToArray();
        }
#pragma warning restore 1591 // Xml Comments
    }
}