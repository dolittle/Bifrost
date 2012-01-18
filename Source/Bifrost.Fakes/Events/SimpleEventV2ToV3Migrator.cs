using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class SimpleEventV2ToV3Migrator : IEventMigrator<v2.SimpleEvent, v3.SimpleEvent>
    {
        public v3.SimpleEvent Migrate(v2.SimpleEvent source)
        {
            var simpleEvent3 = new v3.SimpleEvent(source.EventSourceId, source.Id);
            return simpleEvent3;
        }
    }
}