using Bifrost.Events;
using Bifrost.Testing.Fakes.Events.v2;
using Bifrost.Specs.Events.for_EventMigrationService.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationService
{
    public class when_migrating_a_second_generation_event_that_has_a_single_migrator_registered : an_event_with_a_migrator
    {
        static IEvent result;

        Because of = () => result = event_migrator_manager.Migrate(source_event);

        It should_migrate_the_event_to_the_second_generation_type = () => result.ShouldBeOfExactType(typeof(SimpleEvent));
        It should_migrate_the_correct_values = () =>
                                                   {
                                                       var v2 = result as SimpleEvent;
                                                       v2.EventSourceId.ShouldEqual(source_event.EventSourceId);
                                                       v2.SecondGenerationProperty.ShouldEqual(SimpleEvent.DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY);
                                                   };
    }
}