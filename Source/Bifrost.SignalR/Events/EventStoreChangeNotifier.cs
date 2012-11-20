using System;
using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.SignalR.Commands;
using SignalR;

namespace Bifrost.SignalR.Events
{
    public class EventStoreChangeNotifier : IEventStoreChangeNotifier
    {
        ICommandContextConnectionManager _commandContextConnectionManager;

        public EventStoreChangeNotifier(ICommandContextConnectionManager commandContextConnectionManager)
        {
            _commandContextConnectionManager = commandContextConnectionManager;
        }

        public void Notify(IEventStore eventStore, EventStream streamOfEvents)
        {
            var commandContextsToNotify = new List<Guid>();

            foreach (var @event in streamOfEvents)
                if (!commandContextsToNotify.Contains(@event.CommandContext))
                    commandContextsToNotify.Add(@event.CommandContext);

            foreach (var commandContext in commandContextsToNotify)
            {
                var connectionId = _commandContextConnectionManager.GetConnectionForCommandContext(commandContext);
                CommandCoordinator.EventsProcessed(connectionId, commandContext);
            }
        }
    }
}
