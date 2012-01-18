using v2 = Bifrost.Fakes.Events.v2;
using v3 = Bifrost.Fakes.Events.v3;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_adding_two_migration_levels_to_the_hierachy : given.an_initialized_event_migration_hierarchy
    {
        Because of = () =>
                            {
                                event_migration_hierarchy.AddMigrationLevel(typeof(v2.SimpleEvent));
                                event_migration_hierarchy.AddMigrationLevel(typeof(v3.SimpleEvent));
                            };

        It should_have_still_have_the_logical_event_set_correctly = () => event_migration_hierarchy.LogicalEvent.ShouldEqual(hierarchy_for_type);
        It should_have_a_migration_level_of_two = () => event_migration_hierarchy.MigrationLevel.ShouldEqual(2);
    }
}