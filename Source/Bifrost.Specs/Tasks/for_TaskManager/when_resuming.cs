using System;
using Bifrost.Tasks;
using Machine.Specifications;

namespace Bifrost.Specs.Tasks.for_TaskManager
{
    public class when_resuming : given.a_task_manager_with_one_reporter
    {
        static TaskId task_id = Guid.NewGuid();
        static OurTask task;
        static OurTask result;

        Establish context = () => {
            task = new OurTask
            {
                Id = task_id,
                CurrentOperation = 1
            };
            task_repository_mock.Setup(t=>t.Load(task_id)).Returns(task);
        };

        Because of = () => result = task_manager.Resume<OurTask>(task_id);

        It should_return_the_task = () => result.ShouldEqual(task);
        It should_call_begin_on_the_task = () => result.BeginCalled.ShouldBeTrue();
        It should_execute_the_task = () => task_scheduler_mock.Verify(t => t.Start(task,null), Moq.Times.Once());
        It should_call_the_status_reporter = () => task_status_reporter_mock.Verify(t => t.Resumed(task), Moq.Times.Once());
    }
}
