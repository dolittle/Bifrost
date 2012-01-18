using System;
using Machine.Specifications;
using v2 = Bifrost.Fakes.Events.v2;
using v3 = Bifrost.Fakes.Events.v3;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_getting_the_concrete_type_for_a_level : given.an_event_migration_hierarchy_with_two_levels
    {
        static Type level_one_type;
        static Type level_two_type;

        Because of = () =>
                         {
                             level_one_type = event_migration_hierarchy.GetConcreteTypeForLevel(1);
                             level_two_type = event_migration_hierarchy.GetConcreteTypeForLevel(2);
                         };

        It should_get_the_correct_type_for_level_one = () => level_one_type.ShouldEqual(typeof(v2.SimpleEvent));
        It should_get_the_correct_type_for_level_two = () => level_two_type.ShouldEqual(typeof(v3.SimpleEvent));
    }
}