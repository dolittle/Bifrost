using System;

namespace Bifrost.SignalR.Commands
{
    public interface ICommandContextConnectionManager
    {
        void Register(string connectionId, Guid commandContext);
        void RemoveConnectionIfPresent(string connectionId);
        bool HasConnectionForCommandContext(Guid commandContext);
        string GetConnectionForCommandContext(Guid commandContext);
    }
}
