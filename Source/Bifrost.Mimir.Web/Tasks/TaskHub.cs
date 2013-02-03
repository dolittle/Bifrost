using Bifrost.Tasks;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Bifrost.Mimir.Web.Tasks
{
    [HubName("Tasks")]
    public class TaskHub : Hub
    {
        ITaskRepository _taskRepository;

        public TaskHub(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public IEnumerable<dynamic> GetAllTasks()
        {
            return _taskRepository.LoadAll().Select(t=>ToDynamic(t));
        }

        public static void Started(Task task)
        {
            GlobalHost.ConnectionManager.GetHubContext<TaskHub>().Clients.All.Started(ToDynamic(task));
        }

        public static void Stopped(Task task)
        {
            GlobalHost.ConnectionManager.GetHubContext<TaskHub>().Clients.All.Stopped(ToDynamic(task));
        }

        public static void StateChanged(Task task)
        {
            GlobalHost.ConnectionManager.GetHubContext<TaskHub>().Clients.All.StateChanged(ToDynamic(task));
        }

        static dynamic ToDynamic(Task task)
        {
            dynamic dynamicTask = new ExpandoObject();
            dynamicTask.Type = task.GetType().Name;
            dynamicTask.Id = task.Id.Value;
            dynamicTask.CurrentOperation = task.CurrentOperation;
            PopulateStateFromProperties(dynamicTask, task);
            return dynamicTask;
        }

        static void PopulateStateFromProperties(ExpandoObject target, Task source)
        {
            var sourceType = source.GetType();
            var taskType = typeof(Task);
            var taskProperties = taskType.GetProperties();
            var declaringTypeProperties = sourceType.GetProperties().Where(p => p.DeclaringType == sourceType);
            var sourceProperties = declaringTypeProperties.Where(p => !taskProperties.Any(pp => pp.Name == p.Name));
            var dictionary = sourceProperties.ToDictionary(p => p.Name, p => p.GetValue(source, null).ToString());

            foreach (var key in dictionary.Keys)
                ((IDictionary<string, object>)target)[key] = dictionary[key];
        }

    }
}