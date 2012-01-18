using System;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Bifrost.Specs.Events.for_EventMigrationLevelDiscoverer.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchyManager.given
{
    public class an_event_migration_hierarchy_manager_with_two_logical_events : an_event_migration_hierarchy_discoverer_with_two_logical_events_one_of_which_is_migrated
    {
        protected static IEventMigrationHierarchyManager event_migration_hierarchy_manager;
        protected static Type event_without_migrations = typeof(AnotherSimpleEvent);
        protected static Type event_with_migrations = typeof(SimpleEvent);

        Establish context = () =>
                                {
                                    event_migration_hierarchy_manager = new EventMigrationHierarchyManager(event_migration_level_discoverer);
                                };
    }
}