using System.Collections.Generic;
using System.Linq;
using Bifrost.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext
{
    public class when_getting_objects_being_tracked : given.a_saga_command_context_with_one_object_being_tracked
    {
        static IEnumerable<IAggregateRoot> tracked_objects;

        Because of = () => tracked_objects = command_context.GetObjectsBeingTracked();

        It should_get_one_tracked_object = () => tracked_objects.Count().ShouldEqual(1);
        It should_get_tracked_object = () => tracked_objects.First().ShouldEqual(aggregated_root_mock.Object);
    }
}