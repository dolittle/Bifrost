using System;
using Bifrost.Events;
using Bifrost.NHibernate.Events;
using Bifrost.Serialization;
using Machine.Specifications;
using Moq;

namespace Bifrost.NHibernate.Specs.Events.for_EventConverter.given
{
    public class an_event_converter
    {
        protected static Mock<ISerializer>  serializer_mock;
        protected static Mock<IEventMigratorManager> event_migrator_manager_mock;
        protected static Mock<IEventMigrationHierarchyManager> event_migration_hierarchy_manager;
        protected static EventConverter converter;

        Establish context = () =>
        {
            serializer_mock = new Mock<ISerializer>();
            event_migrator_manager_mock = new Mock<IEventMigratorManager>();
            event_migration_hierarchy_manager = new Mock<IEventMigrationHierarchyManager>();

            event_migration_hierarchy_manager.Setup(e => e.GetLogicalTypeForEvent(Moq.It.IsAny<Type>())).Returns((Type t) => t);

            converter = new EventConverter(serializer_mock.Object, event_migrator_manager_mock.Object, event_migration_hierarchy_manager.Object);
        };
    }
}
