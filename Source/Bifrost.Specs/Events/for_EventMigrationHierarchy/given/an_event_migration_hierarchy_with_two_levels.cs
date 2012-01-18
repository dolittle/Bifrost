using Machine.Specifications;
using v2 = Bifrost.Fakes.Events.v2;
using v3 = Bifrost.Fakes.Events.v3;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy.given
{
    public class an_event_migration_hierarchy_with_two_levels : an_initialized_event_migration_hierarchy
    {
        Establish context = () =>
                                {
                                    event_migration_hierarchy.AddMigrationLevel(typeof(v2.SimpleEvent));
                                    event_migration_hierarchy.AddMigrationLevel(typeof(v3.SimpleEvent));
                                };
    }
}