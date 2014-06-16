using Bifrost.Tasks;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Tasks.for_TaskContext.given
{
    public class a_task_context_with_a_task_and_associated_data
    {
        protected static TaskContext task_context;
        protected static Mock<ITask> task_mock;
        protected static object associated_data = "Something";

        Establish context = () =>
        {
            task_mock = new Mock<ITask>();
            task_context = new TaskContext(task_mock.Object, associated_data);
        };
    }
}
