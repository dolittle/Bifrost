using Bifrost.Events;
using Machine.Specifications;
using Moq;
using Bifrost.Entities;

namespace Bifrost.Specs.Events.for_EventStore.given
{
    public class an_event_store_with_persisted_events : a_persisted_stream_of_20_events_belonging_to_2_different_aggregate_roots
    {
        protected static EventStore event_store;
        protected static Mock<IEventStoreChangeManager> event_store_change_manager_mock;
        protected static Mock<IEntityContext<IEvent>> entity_context_mock;
        protected static Mock<IEventMigrationHierarchyManager> event_migration_hierarchy_manager_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;
        

    	Establish context = () =>
    	                    	{
                                    entity_context_mock = new Mock<IEntityContext<IEvent>>();
                                    entity_context_mock.Setup(context => context.Entities).Returns(persisted_events);

                                    event_migration_hierarchy_manager_mock = new Mock<IEventMigrationHierarchyManager>();
                                    event_store_change_manager_mock = new Mock<IEventStoreChangeManager>();
                                    event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();
                                    event_store = new EventStore(entity_context_mock.Object, event_migration_hierarchy_manager_mock.Object, event_store_change_manager_mock.Object, event_subscription_manager_mock.Object, localizer_mock.Object);

    	                    	};
    }
}