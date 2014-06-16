using Bifrost.Tasks;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Tasks.for_TaskContext
{
    public class when_cancelling_a_task_after_cancelled_and_completed_callback_registered : given.a_task_context_with_a_task_and_associated_data
    {
        static bool cancelled = false;
        static bool completed = false;

        Establish context = () => task_context.Cancelled(c => cancelled = true).Completed(c => completed = true);

        Because of = () => task_context.Cancel();

        It should_set_status_to_cancelled = () => task_context.Status.ShouldEqual(TaskStatus.Cancelled);
        It should_have_cancelled = () => cancelled.ShouldBeTrue();
        It should_have_completed = () => completed.ShouldBeTrue();
    }
}
