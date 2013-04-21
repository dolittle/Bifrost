using System;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventMigrationManager.given
{
    public abstract class an_event_migrator_service_with_no_registered_migrators
    {
        protected static SimpleEvent source_event;
        protected static Guid event_source_id;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static EventMigratorManager event_migrator_manager;

        protected Establish context = () =>
                                {
                                    event_source_id = Guid.NewGuid();
                                    source_event = new SimpleEvent(event_source_id);
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    container_mock = new Mock<IContainer>();
                                    container_mock.Setup(c => c.Get(Moq.It.IsAny<Type>())).Returns(
                                        (Type t) => Activator.CreateInstance(t));

                                    event_migrator_manager = new EventMigratorManager(type_discoverer_mock.Object, container_mock.Object);
                                };
    }
}
