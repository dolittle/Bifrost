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
                _eventStoreChangeManager.NotifyChanges(this);
            }
		}

	    public EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId)
	    {
	        return _repository.GetLastCommittedVersion(aggregatedRootType, aggregateId);
	    }
#pragma warning restore 1591 // Xml Comments
	}
}