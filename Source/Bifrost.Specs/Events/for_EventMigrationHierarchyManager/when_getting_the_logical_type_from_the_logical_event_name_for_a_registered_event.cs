using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchyManager
{
    public class when_getting_the_logical_type_from_the_logical_event_name_for_a_registered_event
        : given.an_event_migration_hierarchy_manager_with_two_logical_events
    {
        static Type event_type;

        Because of = () =>
        {
            event_type = event_migration_hierarchy_manager.GetLogicalTypeFromName("SimpleEvent");
        };

        It should_get_the_correct_logical_event = () =>  event_type.ShouldEqual(typeof (SimpleEvent));
    }
}