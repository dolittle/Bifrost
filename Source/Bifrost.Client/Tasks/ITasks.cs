using System.Collections.Generic;

namespace Bifrost.Tasks
{
    public interface ITasks
    {
        IEnumerable<TaskResult> Results { get; }

        bool IsBusy { get; }

        void Execute(ITask task);

    }
}
