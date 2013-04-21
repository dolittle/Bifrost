using System;
using Machine.Specifications;
using Bifrost.Tasks;

namespace Bifrost.Specs.Tasks.for_TaskScheduler
{
    [Subject(typeof(TaskScheduler))]
    public class when_stopping_synchronous_task_with_two_operations_after_first_operation : given.a_task_scheduler
    {
        static TaskWithTwoOperations task;

        Establish context = () =>
        {
            task = new TaskWithTwoOperations(false);
            task.FirstOperationCallback = () => task_scheduler.Stop(task);
            scheduler_mock.Setup(s => 
                s.Start<Task>(Moq.It.IsAny<Action<Task>>(), task, Moq.It.IsAny<Action<Task>>())).Callback((Action<Task> a, Task t,Action<Task> d) => a(t));
            
        };

        Because of = () => task_scheduler.Start(task);

        It should_get_correct_index_for_first_operation = () => task.FirstOperationIndex.ShouldEqual(0);
        It should_not_execute_second_operation = () => task.SecondOperationCalled.ShouldBeFalse();
        It should_update_the_tasks_current_operation = () => task.CurrentOperation.ShouldEqual(1);
    }
}
