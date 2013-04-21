using System;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchyManager
{
    public class when_getting_the_concrete_type_for_a_registered_event_with_a_valid_level : given.an_event_migration_hierarchy_manager_with_two_logical_events
    {
        static Type concrete_type_for_level_one;
        static Type concrete_type_for_level_two;
        static Type concrete_type_for_level_zero;

        Because of = () =>
        {
            concrete_type_for_level_zero = event_migration_hierarchy_manager.GetConcreteTypeForLogicalEventMigrationLevel(event_with_migrations, 0);
            concrete_type_for_level_one = event_migration_hierarchy_manager.GetConcreteTypeForLogicalEventMigrationLevel(event_with_migrations, 1);
            concrete_type_for_level_two = event_migration_hierarchy_manager.GetConcreteTypeForLogicalEventMigrationLevel(event_with_migrations, 2);
        };

        It should_get_the_logical_type_when_asking_for_level_zero = () => concrete_type_for_level_zero.ShouldEqual(typeof(SimpleEvent));
        It should_get_the_correct_concrete_type_for_level_one = () => concrete_type_for_level_one.ShouldEqual(typeof(Testing.Fakes.Events.v2.SimpleEvent));
        It should_get_the_correct_concrete_type_for_level_two = () => concrete_type_for_level_two.ShouldEqual(typeof(Testing.Fakes.Events.v3.SimpleEvent));
    }
}