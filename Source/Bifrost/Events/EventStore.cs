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
using Bifrost.Execution;
using Bifrost.Globalization;

namespace Bifrost.Events
{
	/// <summary>
	/// Represents an <see cref="IEventStore"/>
	/// </summary>
    public class EventStore : IEventStore
    {
        readonly IEventRepository _repository;
        readonly IEventStoreChangeManager _eventStoreChangeManager;
        readonly IEventSubscriptionManager _eventSubscriptionManager;
	    readonly ILocalizer _localizer;

	    /// <summary>
	    /// Initializes a new instance of <see cref="EventStore"/>
	    /// </summary>
	    /// <param name="repository"><see cref="IEventRepository"/> that persists events</param>
        /// <param name="eventStoreChangeManager">A <see cref="IEventStoreChangeManager"/> for managing changes to the event store</param>
        /// <param name="eventSubscriptionManager">A <see cref="IEventSubscriptionManager"/> for managing event subscriptions</param>
	    /// <param name="localizer"><see cref="ILocalizer" /> that ensures thread has the correct culture.</param>
	    public EventStore(
            IEventRepository repository, 
            IEventStoreChangeManager eventStoreChangeManager, 
            IEventSubscriptionManager eventSubscriptionManager,
            ILocalizer localizer)
        {
            _repository = repository;
            _eventStoreChangeManager = eventStoreChangeManager;
            _eventSubscriptionManager = eventSubscriptionManager;
		    _localizer = localizer;
        }

#pragma warning disable 1591 // Xml Comments
		public CommittedEventStream Load(Type aggregatedRootType, Guid aggregateId)
        {
            var events = _repository.GetForAggregatedRoot(aggregatedRootType, aggregateId);
            var stream = new CommittedEventStream(aggregateId);
            stream.Append(events);
            return stream;
        }

        public void Save(UncommittedEventStream eventsToSave)
        {
            using (_localizer.BeginScope())
            {
                _repository.Insert(eventsToSave);
                _eventSubscriptionManager.Process(eventsToSave);
                _eventStoreChangeManager.NotifyChanges(this, eventsToSave);
            }
		}

	    public EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId)
	    {
	        return _repository.GetLastCommittedVersion(aggregatedRootType, aggregateId);
	    }
#pragma warning restore 1591 // Xml Comments


    }
}