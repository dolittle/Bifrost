using System;

namespace Bifrost.Commands
{
    public interface INotifyEventsProcessed
    {
        event EventsProcessed EventsProcessed;

        void OnEventsProcessed(Guid commandContextId);
    }
}
