using System;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Fakes.Events;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Events.for_EventMigrationManager.given
{
    public abstract class an_event_migrator_service_with_no_registered_migrators
    {
        protected static SimpleEvent source_event;
        protected static Guid event_source_id;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IServiceLocator> service_locator_mock;
        protected static EventMigratorManager event_migrator_manager;

        protected Establish context = () =>
                                {
                                    event_source_id = Guid.NewGuid();
                                    source_event = new SimpleEvent(event_source_id);
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    service_locator_mock = new Mock<IServiceLocator>();
                                    service_locator_mock.Setup(s => s.GetInstance(Moq.It.IsAny<Type>())).Returns(
                                        (Type t) => Activator.CreateInstance(t));

                                    event_migrator_manager = new EventMigratorManager(type_discoverer_mock.Object, service_locator_mock.Object);
                                };
    }
}
