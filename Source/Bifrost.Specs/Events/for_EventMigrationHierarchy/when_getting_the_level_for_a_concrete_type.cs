using System;
using Bifrost.Fakes.Events.v2;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationHierarchy
{
    public class when_getting_the_level_for_a_concrete_type : given.an_event_migration_hierarchy_with_two_levels
    {
        static Type level_one_type;
        static Type level_two_type;
        static int level_one;
        static int level_two;

        Because of = () =>
                         {
                             level_one_type = typeof(SimpleEvent);
                             level_two_type = typeof(Fakes.Events.v3.SimpleEvent);
                             level_one = event_migration_hierarchy.GetLevelForConcreteType(level_one_type);
                             level_two = event_migration_hierarchy.GetLevelForConcreteType(level_two_type);
                         };

        It should_get_the_level_one_for_the_first_migration_type = () => level_one.ShouldEqual(1);
        It should_get_the_correct_type_for_level_two = () => level_two.ShouldEqual(2);
    }
}