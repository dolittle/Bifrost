using Bifrost.Tasks;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Tasks.for_TaskContext
{
    public class when_failing_a_task_before_failed_and_completed_callback_registered : given.a_task_context_with_a_task_and_associated_data
    {
        static bool failed = false;
        static bool completed = false;

        Establish context = () => task_context.Fail();

        Because of = () => task_context.Failed(c => failed = true).Completed(c => completed = true);

        It should_set_status_to_failed = () => task_context.Status.ShouldEqual(TaskStatus.Failed);
        It should_have_failed = () => failed.ShouldBeTrue();
        It should_have_completed = () => completed.ShouldBeTrue();
    }
}
