using Bifrost.Execution;
using Bifrost.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Tasks.for_Tasks
{
    public class when_executing_task_that_finishes
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
            promise.Signal();
        };

        Because of = () => result = tasks.Execute(task_mock.Object);

        It should_not_hold_the_task = () => tasks.All.ShouldNotContain(task_mock.Object);
        It should_not_be_busy = () => tasks.IsBusy.ShouldEqual(false);
    }
}
