using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_initializing_an_event_migration_hierarchy : given.an_initialized_event_migration_hierarchy
    {
        It should_have_the_correct_logical_event_type = () => event_migration_hierarchy.LogicalEvent.ShouldEqual(typeof(SimpleEvent));
        It should_have_a_migration_level_of_zero = () => event_migration_hierarchy.MigrationLevel.ShouldEqual(0);
    }
}