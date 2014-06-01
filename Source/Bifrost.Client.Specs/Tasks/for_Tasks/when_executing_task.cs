using Bifrost.Execution;
using Bifrost.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Tasks.for_Tasks
{
    public class when_executing_task
    {
        static Bifrost.Tasks.Tasks tasks;
        static Mock<ITask> task_mock;
        static TaskContext result;
        static Promise promise;

        Establish context = () =>
        {
            promise = new Promise();
            tasks = new Bifrost.Tasks.Tasks();
            task_mock = new Mock<ITask>();
            task_mock.Setup(t => t.Execute(Moq.It.IsAny<TaskContext>())).Returns(promise);
        };

        Because of = () => result = tasks.Execute(task_mock.Object);

        It should_execute_the_task = () => task_mock.Verify(t=>t.Execute(Moq.It.IsAny<TaskContext>()), Times.Once());
        It should_add_the_task_to_all = () => tasks.All.ShouldContain(task_mock.Object);
        It should_return_a_task_context = () => result.ShouldNotBeNull();
        It should_be_busy = () => tasks.IsBusy.ShouldEqual(true);
    }
}
