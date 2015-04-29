using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Bifrost.Specs.Events.for_EventMigrationService.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationService
{
    public class when_migrating_a_third_generation_event_with_two_migrators_registered : an_event_with_a_migrator
    {
        static IEvent result;

        Establish context = () => event_migrator_manager.RegisterMigrator(typeof(SimpleEventV2ToV3Migrator));

        Because of = () => result = event_migrator_manager.Migrate(source_event);

        It should_migrate_the_event_to_the_second_generation_type = () => result.ShouldBeOfExactType(typeof(SimpleEvent));
        It should_migrate_the_correct_values = () =>
        {
            var v3 = result as Testing.Fakes.Events.v3.SimpleEvent;
            v3.Id.ShouldEqual(source_event.Id);
            v3.EventSourceId.ShouldEqual(source_event.EventSourceId);
            v3.SecondGenerationProperty.ShouldEqual(Testing.Fakes.Events.v2.SimpleEvent.DEFAULT_VALUE_FOR_SECOND_GENERATION_PROPERTY);
            v3.ThirdGenerationProperty.ShouldEqual(Testing.Fakes.Events.v3.SimpleEvent.DEFAULT_VALUE_FOR_THIRD_GENERATION_PROPERTY);
        };
    }
}