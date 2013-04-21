using System;
using System.Linq;
using Bifrost.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class when_deleting_task : given.a_task_repository
    {
        static TaskEntity task_entity;
        static MyTask task;

        Establish context = () =>
        {
            task = new MyTask
            {
                Id = Guid.NewGuid(),
                CurrentOperation = 42,
                AString = "Hello world",
                AnInteger = 43
            };

            entity_context_mock.Setup(e => e.GetById(task.Id.Value)).Returns(new TaskEntity { Id = task.Id });
            entity_context_mock.Setup(e=>e.Delete(Moq.It.IsAny<TaskEntity>())).Callback((TaskEntity t) => task_entity = t);
        };

        Because of = () => repository.Delete(task);

        It should_delete_with_correct_id = () => task_entity.Id.ShouldEqual(task.Id.Value);
    }
}
