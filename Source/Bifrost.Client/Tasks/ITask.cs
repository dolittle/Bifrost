using Bifrost.Execution;

namespace Bifrost.Tasks
{
    public interface ITask
    {
        Promise Execute(TaskContext context);
    }
}
