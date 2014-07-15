using System;
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
        static Mock<IDispatcher> dispatcher_mock;

        Establish context = () =>
        {
            promise = new Promise();

            dispatcher_mock = new Mock<IDispatcher>();
            dispatcher_mock.Setup(d => d.BeginInvoke(Moq.It.IsAny<Action>())).Callback<Action>(a => a());

            tasks = new Bifrost.Tasks.Tasks(dispatcher_mock.Object);
            task_mock = new Mock<ITask>();
            task_mock.Setup(t => t.Execute(Moq.It.IsAny<TaskContext>())).Returns(promise);
            promise.Signal();
        };

        Because of = () => result = tasks.Execute(task_mock.Object);

        It should_not_hold_the_task = () => tasks.All.ShouldNotContain(task_mock.Object);
        It should_not_hold_the_task_context = () => tasks.Contexts.ShouldNotContain(result);
        It should_not_be_busy = () => tasks.IsBusy.ShouldEqual(false);
    }
}
