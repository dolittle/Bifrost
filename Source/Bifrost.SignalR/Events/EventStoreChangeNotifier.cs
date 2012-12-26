using System;
using System.Collections.Generic;
using Bifrost.Events;

namespace Bifrost.SignalR.Events
{
    public class EventStoreChangeNotifier : IEventStoreChangeNotifier
    {
        ICommandCoordinatorEvents _commandCoordinatorEvents;

        public EventStoreChangeNotifier(ICommandCoordinatorEvents commandCoordinatorEvents)
        {
            _commandCoordinatorEvents = commandCoordinatorEvents;
        }

        public void Notify(IEventStore eventStore, EventStream streamOfEvents)
        {
            var commandContextsToNotify = GetUniqueCommandContextsFromEvents(streamOfEvents);
            foreach (var commandContext in commandContextsToNotify)
                _commandCoordinatorEvents.EventsProcessed(commandContext);
        }

        List<Guid> GetUniqueCommandContextsFromEvents(EventStream streamOfEvents)
        {
            var commandContextsToNotify = new List<Guid>();
            foreach (var @event in streamOfEvents)
                if (!commandContextsToNotify.Contains(@event.CommandContext))
                    commandContextsToNotify.Add(@event.CommandContext);

            return commandContextsToNotify;
        }
    }
}
