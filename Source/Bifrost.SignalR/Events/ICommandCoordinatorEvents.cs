using System;

namespace Bifrost.SignalR.Events
{
    public interface ICommandCoordinatorEvents
    {
        void EventsProcessed(Guid commandContext);
    }
}
