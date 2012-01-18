using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class SimpleEventV1ToV2Migrator : IEventMigrator<SimpleEvent, v2.SimpleEvent>
    {
        public v2.SimpleEvent Migrate(SimpleEvent source)
        {
            var simpleEvent2 = new v2.SimpleEvent(source.EventSourceId, source.Id);
            return simpleEvent2;
        }
    }
}