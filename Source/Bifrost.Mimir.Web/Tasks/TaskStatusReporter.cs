using Bifrost.Tasks;

namespace Bifrost.Mimir.Web.Tasks
{
    public class TaskStatusReporter : ITaskStatusReporter
    {
        public void Started(Task task)
        {
            TaskHub.Started(task);
        }

        public void Stopped(Task task)
        {
            TaskHub.Stopped(task);
        }

        public void Paused(Task task)
        {
            
        }

        public void Resumed(Task task)
        {
            
        }

        public void StateChanged(Task task)
        {
            TaskHub.StateChanged(task);
        }
    }
}