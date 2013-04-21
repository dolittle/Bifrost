using Bifrost.Concurrency;
using Bifrost.Tasks;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Tasks.for_TaskScheduler.given
{
    public class a_task_scheduler
    {
        protected static Mock<IScheduler> scheduler_mock;
        protected static TaskScheduler task_scheduler;

        Establish context = () =>
        {
            scheduler_mock = new Mock<IScheduler>();
            task_scheduler = new TaskScheduler(scheduler_mock.Object);
        };
    }
}
