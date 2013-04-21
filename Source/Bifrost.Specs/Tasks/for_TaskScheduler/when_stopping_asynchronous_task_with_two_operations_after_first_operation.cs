using System;
using Machine.Specifications;
using Bifrost.Tasks;

namespace Bifrost.Specs.Tasks.for_TaskScheduler
{
    [Subject(typeof(TaskScheduler))]
    public class when_stopping_asynchronous_task_with_two_operations_after_first_operation : given.a_task_scheduler
    {
        static TaskWithTwoOperations task;
        static Guid[] guids = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
        static int action_count = 0;

        Establish context = () =>
        {
            task = new TaskWithTwoOperations(true);
            task.FirstOperationCallback = () => task_scheduler.Stop(task);
            scheduler_mock.Setup(s => 
                s.Start<Task>(Moq.It.IsAny<Action<Task>>(), task, Moq.It.IsAny<Action<Task>>())).Returns((Action<Task> a, Task t,Action<Task> d) => {
                    var id = guids[action_count];
                    a(t);
                    d(t);
                    action_count++;
                    return id;
                });
        };

        Because of = () => task_scheduler.Start(task);

        It should_call_stop_for_second_operation = () => scheduler_mock.Verify(s => s.Stop(guids[1]), Moq.Times.Once());        
    }
}
