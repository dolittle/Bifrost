using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Bifrost.Configuration;
using Bifrost.Events;
using Bifrost.Entities;
using Bifrost.Fakes.Configuration;
using Bifrost.Fakes.Entities;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration
{
    [Subject(typeof(EventsConfiguration))]
    public class when_initializing_with_storage: given.an_events_configuration_and_container_object
    {
        static IEntityContextConfiguration entity_context_configuration; 

        Establish context = () => 
                                {
                                    entity_context_configuration = new EntityContextConfiguration
                                                                        {
                                                                            Connection = new EntityContextConnection(),
                                                                            EntityContextType = typeof(EntityContext<>)
                                                                        };
                                    events_configuration.EntityContextConfiguration = entity_context_configuration; 
                                    
                                };

        Because of = () => events_configuration.Initialize(container_mock.Object);

        It should_bind_the_specific_storage_for_events = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<EventHolder>), Moq.It.IsAny<Type>()));
        It should_bind_the_specific_storage_for_event_subscriptions= () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<EventSubscriptionHolder>), Moq.It.IsAny<Type>()));

    }
}
