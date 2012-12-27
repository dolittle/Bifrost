using System;

namespace Bifrost.Commands
{
    public interface INotifyCommandResultsReceived
    {
        event CommandResultsReceived CommandResultsReceived;

        void OnCommandResultsReceived(Guid commandContextId, CommandResult result);
    }
}
