using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy.given
{
    public class an_initialized_event_migration_hierarchy
    {
        protected static Type hierarchy_for_type;
        protected static EventMigrationHierarchy event_migration_hierarchy;

        private Establish context = () =>
                                        {
                                            hierarchy_for_type = typeof (Fakes.Events.SimpleEvent);
                                            event_migration_hierarchy = new EventMigrationHierarchy(hierarchy_for_type);
                                        };
    }
}