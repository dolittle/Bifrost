using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationLevelDiscoverer
{
    public class when_getting_the_event_migration_hierarchies : given.an_event_migration_hierarchy_discoverer_with_two_logical_events_one_of_which_is_migrated
    {
        static IEnumerable<EventMigrationHierarchy> event_migration_hierarchies;
        static EventMigrationHierarchy non_migrated_event_hierarchy;
        static EventMigrationHierarchy migrated_event_hierarchy;

        Because of = () =>
                         {
                             event_migration_hierarchies = event_migration_level_discoverer.GetMigrationHierarchies();
                             non_migrated_event_hierarchy = event_migration_hierarchies.Where(e => e.LogicalEvent == typeof(AnotherSimpleEvent)).First();
                             migrated_event_hierarchy = event_migration_hierarchies.Where(e => e.LogicalEvent == typeof(SimpleEvent)).First();
                         };

        It should_create_two_seperate_hierarchies = () => event_migration_hierarchies.Count().ShouldEqual(2);
        It should_create_a_hierarchy_with_no_children_for_the_event_that_is_not_migrated = () => non_migrated_event_hierarchy.MigrationLevel.ShouldEqual(0);
        It should_create_a_hierarchy_with_children_for_the_event_that_is_migrated = () => migrated_event_hierarchy.MigrationLevel.ShouldEqual(2);
        It should_order_the_event_generations_correctly_for_the_migrated_event = () =>
                                            {
                                                migrated_event_hierarchy.GetConcreteTypeForLevel(0).ShouldEqual(typeof(SimpleEvent));
                                                migrated_event_hierarchy.GetConcreteTypeForLevel(1).ShouldEqual(typeof(Testing.Fakes.Events.v2.SimpleEvent));
                                                migrated_event_hierarchy.GetConcreteTypeForLevel(2).ShouldEqual(typeof(Testing.Fakes.Events.v3.SimpleEvent));
                                            };
    }
}