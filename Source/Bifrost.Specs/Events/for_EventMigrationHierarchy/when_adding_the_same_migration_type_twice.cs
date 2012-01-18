using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_adding_the_same_migration_type_twice : given.an_initialized_event_migration_hierarchy
    {
        static Exception Exception;

        Because of = () =>
                         {
                             event_migration_hierarchy.AddMigrationLevel(typeof(Fakes.Events.v2.SimpleEvent));
                             Exception = Catch.Exception(() => event_migration_hierarchy.AddMigrationLevel(typeof(Fakes.Events.v2.SimpleEvent)));
                         };

        It should_throw_a_duplicate_in_event_migration_hierarchy_exception = () => Exception.ShouldBeOfType(typeof(DuplicateInEventMigrationHierarchyException));
    }
}