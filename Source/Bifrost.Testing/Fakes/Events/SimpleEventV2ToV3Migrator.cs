using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class SimpleEventV2ToV3Migrator : IEventMigrator<Testing.Fakes.Events.v2.SimpleEvent, Testing.Fakes.Events.v3.SimpleEvent>
    {
        public v3.SimpleEvent Migrate(v2.SimpleEvent source)
        {
            var simpleEvent3 = new v3.SimpleEvent(source.EventSourceId);
            return simpleEvent3;
        }
    }
}