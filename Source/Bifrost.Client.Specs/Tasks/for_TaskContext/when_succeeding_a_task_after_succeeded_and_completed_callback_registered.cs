using Bifrost.Tasks;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Tasks.for_TaskContext
{
    public class when_succeeding_a_task_after_succeeded_and_completed_callback_registered : given.a_task_context_with_a_task_and_associated_data
    {
        static bool succeeded = false;
        static bool completed = false;

        Establish context = () => task_context.Succeeded(c => succeeded = true).Completed(c => completed = true);

        Because of = () => task_context.Succeed();

        It should_set_status_to_succeeded = () => task_context.Status.ShouldEqual(TaskStatus.Succeeded);
        It should_have_succeeded = () => succeeded.ShouldBeTrue();
        It should_have_completed = () => completed.ShouldBeTrue();
    }
}
