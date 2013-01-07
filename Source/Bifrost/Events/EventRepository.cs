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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Entities;
using System.Linq.Expressions;

// The Mono compiler insists on that the private field "_eventMigrationHierarchyManager" is not used, only assigned to.. Not true!
#pragma warning disable 414	

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IEventRepository"/>
    /// </summary>
    public class EventRepository : IEventRepository
    {
        readonly IEntityContext<IEvent> _entityContext;
        readonly IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

    	/// <summary>
    	/// Initializes a new instance of <see cref="EventRepository"/>
    	/// </summary>
    	/// <param name="entityContext"><see cref="IEntityContext{T}"/> for retrieving events</param>
        /// <param name="eventMigrationHierarchyManager">A <see cref="IEventMigrationHierarchyManager"/> for managing event migrations</param>
        public EventRepository(
            IEntityContext<IEvent> entityContext, 
            IEventMigrationHierarchyManager eventMigrationHierarchyManager)
    	{
    		_entityContext = entityContext;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
    	}

#pragma warning disable 1591 // Xml Comments
        public IEvent GetById(long id)
        {
        	var @event = GetEventById(id);
        	return @event;
        }

        public IEnumerable<IEvent> GetPage(int pageSize, int pageNumber)
        {
            var events = _entityContext.Entities.Skip(pageSize * pageNumber).Take(pageSize);
            return events.ToArray();
        }


    	public IEnumerable<IEvent> GetByIds(IEnumerable<long> ids)
        {
            var events = _entityContext.Entities.Where(e => ids.Contains(e.Id));
            return events.ToArray();
        }

        public IEnumerable<IEvent> GetForAggregatedRoot(Type aggregatedRootType, Guid aggregateId)
        {
            var query = (from e in _entityContext.Entities
                        where e.EventSourceId == aggregateId && e.EventSource == aggregatedRootType.AssemblyQualifiedName
                        select e).ToList();

            return query.ToArray();
        }

        public IEnumerable<IEvent> GetUnprocessedEventsForSubscriptions(IEnumerable<EventSubscription> subscriptions)
        {
            var query = _entityContext.Entities;
            foreach (var subscription in subscriptions)
            {
                var logicalType = _eventMigrationHierarchyManager.GetLogicalTypeFromName(subscription.EventName);
                query = query.Where(e => e.Name == logicalType.Name);
            }
            return query.ToArray();
        }

        public void Insert(IEnumerable<IEvent> events)
        {
            var eventArray = events.ToArray();
            for (var eventIndex = 0; eventIndex < eventArray.Length; eventIndex++)
            {
                var @event = eventArray[eventIndex];
                _entityContext.Insert(@event);
            }

            _entityContext.Commit();
        }

        public EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId)
        {
            var @event = _entityContext.Entities
                .Where(e => e.EventSourceId == aggregateId)
                    .OrderByDescending(e => e.Version)
                .FirstOrDefault();

            if (@event == null)
                return EventSourceVersion.Zero;

            return @event.Version;
        }
#pragma warning restore 1591 // Xml Comments

		IEvent GetEventById(long id)
		{
			var eventHolder = _entityContext.Entities.Where(e => e.Id == id).SingleOrDefault();
			if (eventHolder == null)
				throw new ArgumentException(string.Format("Event with Id '{0}' does not exist", id));

			return eventHolder;
		}
    }
}
