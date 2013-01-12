using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Bifrost.Specs.Events.for_EventMigrationManager.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventMigrationService.given
{
    public abstract class an_event_with_a_migrator : an_event_migrator_service_with_no_registered_migrators
    {
        private Establish context = () => event_migrator_manager.RegisterMigrator(typeof(SimpleEventV1ToV2Migrator));

    }
}