using System;
using Bifrost.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class when_saving_task : given.a_task_repository
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

            entity_context_mock.Setup(e=>e.Insert(Moq.It.IsAny<TaskEntity>())).Callback((TaskEntity t) => task_entity = t);
        };

        Because of = () => repository.Save(task);

        It should_insert_with_correct_id = () => task_entity.Id.ShouldEqual(task.Id.Value);
        It should_insert_with_correct_type = () => task_entity.Type.ShouldEqual(typeof(MyTask));
        It should_insert_with_correct_current_operation = () => task_entity.CurrentOperation.ShouldEqual(task.CurrentOperation);
        It should_insert_with_correct_string_state = () => task_entity.State["AString"].ShouldEqual(task.AString);
        It should_insert_with_correct_integer_state = () => int.Parse(task_entity.State["AnInteger"]).ShouldEqual(task.AnInteger);
    }
}
