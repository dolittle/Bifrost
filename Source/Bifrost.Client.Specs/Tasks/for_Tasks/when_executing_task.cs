using Bifrost.Execution;
using Bifrost.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using System.Linq;
using System;

namespace Bifrost.Client.Specs.Tasks.for_Tasks
{
    public class when_executing_task
    {
        static Bifrost.Tasks.Tasks tasks;
        static Mock<ITask> task_mock;
        static TaskContext result;
        static Promise promise;
        static string associated_data = "Some data associated with the task";
        static Mock<IDispatcher> dispatcher_mock;


        Establish context = () =>
        {
            promise = new Promise();
            dispatcher_mock = new Mock<IDispatcher>();
            dispatcher_mock.Setup(d => d.BeginInvoke(Moq.It.IsAny<Action>())).Callback<Action>(a => a());

            tasks = new Bifrost.Tasks.Tasks(dispatcher_mock.Object);
            task_mock = new Mock<ITask>();
            task_mock.Setup(t => t.Execute(Moq.It.IsAny<TaskContext>())).Returns(promise);
        };

        Because of = () => result = tasks.Execute(task_mock.Object, associated_data);

        It should_execute_the_task = () => task_mock.Verify(t=>t.Execute(Moq.It.IsAny<TaskContext>()), Times.Once());
        It should_add_the_task_to_all = () => tasks.All.ShouldContain(task_mock.Object);
        It should_return_a_task_context = () => result.ShouldNotBeNull();
        It should_be_busy = () => tasks.IsBusy.ShouldEqual(true);
        It should_pass_the_associated_data_to_the_task_context = () => result.AssociatedData.ShouldEqual(associated_data);
        It should_add_a_context = () => tasks.Contexts.Count().ShouldEqual(1);
        It should_add_a_context_for_the_task = () => tasks.Contexts.First().Task.ShouldEqual(task_mock.Object);
    }
}
