using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchyManager
{
    public class when_getting_the_concrete_type_for_a_registered_event_with_an_invalid_level : given.an_event_migration_hierarchy_manager_with_two_logical_events
    {
        static Exception exception_when_level_is_invalid;
        static Exception exception_when_level_does_not_exist;

        Because of = () =>
        {
           exception_when_level_is_invalid = Catch.Exception(() => event_migration_hierarchy_manager.GetConcreteTypeForLogicalEventMigrationLevel(event_without_migrations, -1));
           exception_when_level_does_not_exist = Catch.Exception(() => event_migration_hierarchy_manager.GetConcreteTypeForLogicalEventMigrationLevel(event_without_migrations, 1));
        };

        It should_throw_a_migration_level_out_of_range_exception_when_the_migration_level_has_not_been_reached =
            () => exception_when_level_does_not_exist.ShouldBeOfType(typeof(MigrationLevelOutOfRangeException));

        It should_throw_a_migration_level_out_of_range_exception_when_the_migration_level_is_not_valid =
            () => exception_when_level_is_invalid.ShouldBeOfType(typeof(MigrationLevelOutOfRangeException));
    }
}