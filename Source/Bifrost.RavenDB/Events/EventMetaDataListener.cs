using Bifrost.Events;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Bifrost.RavenDB.Events
{
    public class EventMetaDataListener : IDocumentStoreListener
    {
        const string Generation = "Generation";
        const string LogicalEventType = "LogicalEventType";

        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        public EventMetaDataListener(IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
        }

        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
        {
            var eventType = entityInstance.GetType();
            var logicalEventType = _eventMigrationHierarchyManager.GetLogicalTypeForEvent(eventType);
            var migrationLevel = _eventMigrationHierarchyManager.GetCurrentMigrationLevelForLogicalEvent(logicalEventType);
            metadata[Generation] = migrationLevel;
            metadata[LogicalEventType] = string.Format("{0}, {1}", logicalEventType.FullName, logicalEventType.Assembly.GetName().Name);
            return false;
        }

    }
}
