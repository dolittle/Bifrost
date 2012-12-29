using Machine.Specifications;

namespace Bifrost.Specs.Tasks.for_TaskManager
{
    public class when_starting : given.a_task_manager_with_one_reporter
    {
        static OurTask task;

        Establish context = () => {
            task = new OurTask
            {
                CurrentOperation = 1
            };
        };

        Because of = () => task_manager.Start(task);

        It should_call_begin_on_the_task = () => task.BeginCalled.ShouldBeTrue();
        It should_execute_the_task = () => task_executor_mock.Verify(t => t.Execute(task), Moq.Times.Once());
        It should_reset_current_operation = () => task.CurrentOperation.ShouldEqual(0);
        It should_call_the_status_reporter = () => task_status_reporter_mock.Verify(t => t.Started(task), Moq.Times.Once());
    }
}
