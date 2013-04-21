using System;
using Bifrost.Tasks;
using Machine.Specifications;

namespace Bifrost.Specs.Tasks.for_TaskManager
{
    public class when_pausing : given.a_task_manager_with_one_reporter
    {
        static TaskId task_id = Guid.NewGuid();
        static OurTask task;

        Establish context = () => {
            task = new OurTask
            {
                Id = task_id,
            };
            task_repository_mock.Setup(t=>t.Load(task_id)).Returns(task);
        };

        Because of = () => task_manager.Pause(task_id);

        It should_stop_the_executor = () => task_scheduler_mock.Verify(t => t.Stop(task), Moq.Times.Once());
        It should_call_the_status_reporter = () => task_status_reporter_mock.Verify(t => t.Paused(task), Moq.Times.Once());
    }
}
