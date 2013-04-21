using Bifrost.Tasks;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Tasks.for_TaskManager.given
{
    public class a_task_manager_with_one_reporter
    {
        protected static Mock<ITaskRepository> task_repository_mock;
        protected static Mock<ITaskScheduler> task_scheduler_mock;
        protected static Mock<ITypeImporter> type_importer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ITaskStatusReporter> task_status_reporter_mock;
        protected static TaskManager task_manager;

        Establish context = () =>
        {
            task_repository_mock = new Mock<ITaskRepository>();
            task_scheduler_mock = new Mock<ITaskScheduler>();
            type_importer_mock = new Mock<ITypeImporter>();
            container_mock = new Mock<IContainer>();
            task_status_reporter_mock = new Mock<ITaskStatusReporter>();
            type_importer_mock.Setup(t => t.ImportMany<ITaskStatusReporter>()).Returns(new[] { task_status_reporter_mock.Object });
            task_manager = new TaskManager(task_repository_mock.Object, task_scheduler_mock.Object, type_importer_mock.Object, container_mock.Object);
        };
    }
}
