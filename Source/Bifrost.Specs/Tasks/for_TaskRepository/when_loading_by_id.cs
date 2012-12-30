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
        static TaskEntity[] task_entities;
        static MyTask result;

        Establish context = () =>
        {
            task_entities = new[] {
                new TaskEntity { Id = Guid.NewGuid(), CurrentOperation = 42, State = new Dictionary<string,string> { { "AString", "Hello world" }, { "AnInteger", "42" } },Type = typeof(MyTask) },
                new TaskEntity { Id = Guid.NewGuid(), CurrentOperation = 43, State = new Dictionary<string,string> { { "AString", "Hello second world" }, { "AnInteger", "43" } },Type = typeof(MyTask) }
            };
            container_mock.Setup(c => c.Get(typeof(MyTask))).Returns(() => new MyTask());
            entity_context_mock.Setup(e => e.Entities).Returns(task_entities.AsQueryable());
        };

        Because of = () => result = (MyTask)repository.Load(task_entities[1].Id);

        It should_return_a_task = () => result.ShouldNotBeNull();
        It should_have_id_set_correctly = () => result.Id.Value.ShouldEqual(task_entities[1].Id);
        It should_have_current_operation_set_correctly = () => result.CurrentOperation.ShouldEqual(task_entities[1].CurrentOperation);
        It should_have_string_state_set_correctly = () => result.AString.ShouldEqual(task_entities[1].State["AString"]);
        It should_have_integer_state_set_correctly = () => result.AnInteger.ShouldEqual(int.Parse(task_entities[1].State["AnInteger"]));
    }
}
