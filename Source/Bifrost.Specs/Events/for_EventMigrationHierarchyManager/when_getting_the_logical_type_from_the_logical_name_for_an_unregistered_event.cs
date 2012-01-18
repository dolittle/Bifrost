using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchyManager
{
    public class when_getting_the_logical_type_from_the_logical_name_for_an_unregistered_event
        : given.an_event_migration_hierarchy_manager_with_two_logical_events
    {
        static Exception exception;

        Because of = () =>
                         {
                             exception = Catch.Exception(() => event_migration_hierarchy_manager.GetLogicalTypeFromName("UnregisteredEvent"));
                         };

        It should_throw_an_unregistered_event_exception = () => exception.ShouldBeOfType(typeof(UnregisteredEventException));
    }
}