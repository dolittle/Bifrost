using System;
using Bifrost.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class when_deleting_task_by_id : given.a_task_repository
    {
        static TaskId task_id = Guid.NewGuid();
        static Guid task_entity_id;

        Establish context = () =>
        {
            entity_context_mock.Setup(e=>e.DeleteById(Moq.It.IsAny<Guid>())).Callback((Guid i)=>task_entity_id = i);
        };

        Because of = () => repository.DeleteById(task_id);

        It should_delete_with_correct_id = () => task_entity_id.ShouldEqual(task_id.Value);
    }
}
