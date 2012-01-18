using Bifrost.Fakes.Events.v2;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_adding_a_migration_level_to_the_hierarchy : given.an_initialized_event_migration_hierarchy
    {
        private Because of = () => event_migration_hierarchy.AddMigrationLevel(typeof(SimpleEvent));

        It should_have_still_have_the_logical_event_set_correctly = () => event_migration_hierarchy.LogicalEvent.ShouldEqual(hierarchy_for_type);
        It should_have_a_migration_level_of_one = () => event_migration_hierarchy.MigrationLevel.ShouldEqual(1);
    }
}