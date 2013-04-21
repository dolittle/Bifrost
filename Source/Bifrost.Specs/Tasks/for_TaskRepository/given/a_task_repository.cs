using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.Tasks;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Tasks.for_TaskRepository.given
{
    public class a_task_repository
    {
        protected static Mock<IEntityContext<TaskEntity>> entity_context_mock;
        protected static TaskRepository repository;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
        {
            entity_context_mock = new Mock<IEntityContext<TaskEntity>>();
            container_mock = new Mock<IContainer>();
            repository = new TaskRepository(entity_context_mock.Object, container_mock.Object);
        };
    }
}
