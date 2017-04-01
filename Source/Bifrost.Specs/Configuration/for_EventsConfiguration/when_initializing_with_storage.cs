using System;
using Bifrost.Configuration;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Configuration;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

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

        It should_bind_the_specific_storage_for_events = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<IEvent>), Moq.It.IsAny<Type>()));
    }
}
