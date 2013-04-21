using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class when_loading_by_id : given.a_task_repository
    {
        static TaskEntity task_entity;
        static MyTask result;

        Establish context = () =>
        {
            task_entity = new TaskEntity { Id = Guid.NewGuid(), CurrentOperation = 43, State = new Dictionary<string, string> { { "AString", "Hello second world" }, { "AnInteger", "43" } }, Type = typeof(MyTask) };
            container_mock.Setup(c => c.Get(typeof(MyTask))).Returns(() => new MyTask());
            entity_context_mock.Setup(e => e.GetById(task_entity.Id)).Returns(task_entity);
        };

        Because of = () => result = (MyTask)repository.Load(task_entity.Id);

        It should_return_a_task = () => result.ShouldNotBeNull();
        It should_have_id_set_correctly = () => result.Id.Value.ShouldEqual(task_entity.Id);
        It should_have_current_operation_set_correctly = () => result.CurrentOperation.ShouldEqual(task_entity.CurrentOperation);
        It should_have_string_state_set_correctly = () => result.AString.ShouldEqual(task_entity.State["AString"]);
        It should_have_integer_state_set_correctly = () => result.AnInteger.ShouldEqual(int.Parse(task_entity.State["AnInteger"]));
    }
}
