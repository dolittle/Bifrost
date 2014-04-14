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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Serialization;
using Bifrost.Events;

namespace Bifrost.NHibernate.Events
{
    /// <summary>
    /// Represents an implementation of a <see cref="IEventConverter"/>
    /// </summary>
	public class EventConverter : IEventConverter
	{
		readonly ISerializer _serializer;
		readonly IEventMigratorManager _eventMigratorManager;
		readonly IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        /// <summary>
        /// Initializes a new instance of <see cref="EventConverter"/>
        /// </summary>
        /// <param name="serializer">A <see cref="ISerializer"/> used during conversion for serialization</param>
        /// <param name="eventMigratorManager">A <see cref="IEventMigratorManager"/> for getting migrators for an <see cref="IEvent"/></param>
        /// <param name="eventMigrationHierarchyManager">A <see cref="IEventMigrationHierarchyManager"/> for handling the migration hierarchies for an <see cref="IEvent"/></param>
		public EventConverter(
			ISerializer serializer,
			IEventMigratorManager eventMigratorManager,
			IEventMigrationHierarchyManager eventMigrationHierarchyManager)
		{
			_serializer = serializer;
			_eventMigratorManager = eventMigratorManager;
			_eventMigrationHierarchyManager = eventMigrationHierarchyManager;
		}

#pragma warning disable 1591 // Xml Comments
        public IEvent ToEvent(EventHolder eventHolder)
		{
			var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeFromName(eventHolder.LogicalEventName);
			var concreteType = _eventMigrationHierarchyManager.GetConcreteTypeForLogicalEventMigrationLevel(logicalEventType, eventHolder.Generation);
			var @event = CreateInstance(concreteType, eventHolder.AggregateId);
			_serializer.FromJson(@event, eventHolder.SerializedEvent);
            @event.Id = eventHolder.Id;
			return _eventMigratorManager.Migrate(@event);
		}

		public EventHolder ToEventHolder(IEvent @event)
		{
			var eventHolder = new EventHolder();
			ToEventHolder(eventHolder, @event);
			return eventHolder;
		}

		public void ToEventHolder(EventHolder eventHolder, IEvent @event)
		{
			var eventType = @event.GetType();
			var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
			var generation = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
			var eventSourceName = GetEventSourceFromEvent(@event);
			eventHolder.Id = @event.Id;
			eventHolder.CommandName = @event.CommandName;
			eventHolder.Name = @event.Name;
			eventHolder.AggregateId = @event.EventSourceId;
            eventHolder.EventSource = eventSourceName;
			eventHolder.SerializedEvent = _serializer.ToJson(@event);
			eventHolder.LogicalEventName = logicalEventType.Name;
			eventHolder.Generation = generation;
            eventHolder.CausedBy = @event.CausedBy;
            eventHolder.Origin = @event.Origin;
			eventHolder.Occured = @event.Occured;
			eventHolder.Version = @event.Version.Combine();
		}

		public IEnumerable<EventHolder> ToEventHolders(IEnumerable<IEvent> events)
		{
			var eventHolders = events.Select(ToEventHolder);
			return eventHolders;
		}

		public IEnumerable<IEvent> ToEvents(IEnumerable<EventHolder> eventHolders)
		{
			var events = eventHolders.Select(ToEvent);
			return events;
		}
#pragma warning restore 1591 // Xml Comments


        static string GetEventSourceFromEvent(IEvent @event)
		{
			var eventSource = "[Not Available]";
			if (!string.IsNullOrEmpty(@event.EventSource))
				eventSource = @event.EventSource;
			return eventSource;
		}

		static IEvent CreateInstance(Type eventType, Guid eventSourceId)
		{
			IEvent @event;
			var constructors = eventType.GetConstructors();
			var query = from c in constructors
						where c.GetParameters().Length == 0
						select c;

			var defaultConstructor = query.SingleOrDefault();
			if (null != defaultConstructor)
				@event = Activator.CreateInstance(eventType) as IEvent;
			else
				@event = Activator.CreateInstance(eventType, eventSourceId) as IEvent;

			return @event;
		}
	}
}