using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Bifrost.Configuration;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Entities;
using Bifrost.Events;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration
{
    [Subject(typeof(EventsConfiguration))]
    public class when_initializing_without_storage : given.an_events_configuration_and_container_object
    {

        Establish context = () => 
                    {
                        event_store_change_manager_mock = new Mock<Bifrost.Events.IEventStoreChangeManager>();
                    };  
                    

        Because of = () => events_configuration.Initialize(container_mock.Object);

        It should_be_initialized = () => events_configuration.ShouldNotBeNull();
        It should_not_set_up_storage_for_events = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<IEvent>), Moq.It.IsAny<Type>()), Times.Never());
        It should_not_set_up_storage_for_event_subscriptions = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<EventSubscription>), Moq.It.IsAny<Type>()), Times.Never());
                  

        //It should_call_the_base_initialize_method = () => events_configuration.
    }
}
