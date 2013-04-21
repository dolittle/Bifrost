using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tasks.for_TaskRepository
{
    public class when_loading_all : given.a_task_repository
    {
        static TaskEntity[] task_entities;
        static MyTask[] result;

        Establish context = () =>
        {
            task_entities = new[] {
                new TaskEntity { Id = Guid.NewGuid(), CurrentOperation = 42, State = new Dictionary<string,string> { { "AString", "Hello world" }, { "AnInteger", "42" } },Type = typeof(MyTask) },
                new TaskEntity { Id = Guid.NewGuid(), CurrentOperation = 43, State = new Dictionary<string,string> { { "AString", "Hello second world" }, { "AnInteger", "43" } },Type = typeof(MyTask) }
            };
            container_mock.Setup(c => c.Get(typeof(MyTask))).Returns(() => new MyTask());
            entity_context_mock.Setup(e => e.Entities).Returns(task_entities.AsQueryable());
        };

        Because of = () => result = repository.LoadAll().Select(t=>(MyTask)t).ToArray();

        It should_get_two_tasks = () => result.Length.ShouldEqual(2);
        It should_have_id_set_correctly_for_first_task = () => result[0].Id.Value.ShouldEqual(task_entities[0].Id);
        It should_have_id_set_correctly_for_second_task = () => result[1].Id.Value.ShouldEqual(task_entities[1].Id);
        It should_have_current_operation_set_correctly_for_first_task = () => result[0].CurrentOperation.ShouldEqual(task_entities[0].CurrentOperation);
        It should_have_current_operation_set_correctly_for_second_task = () => result[1].CurrentOperation.ShouldEqual(task_entities[1].CurrentOperation);
        It should_have_string_state_set_correctly_for_first_task = () => result[0].AString.ShouldEqual(task_entities[0].State["AString"]);
        It should_have_string_state_set_correctly_for_second_task = () => result[1].AString.ShouldEqual(task_entities[1].State["AString"]);
        It should_have_integer_state_set_correctly_for_first_task  = () => result[0].AnInteger.ShouldEqual(int.Parse(task_entities[0].State["AnInteger"]));
        It should_have_integer_state_set_correctly_for_second_task = () => result[1].AnInteger.ShouldEqual(int.Parse(task_entities[1].State["AnInteger"]));
    }
}
