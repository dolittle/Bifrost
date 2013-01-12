using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Testing.Fakes.Configuration;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;
using Bifrost.Configuration;
using Bifrost.Events;
using Bifrost.Entities;
using Bifrost.Sagas;

namespace Bifrost.Specs.Configuration.for_SagasConfiguration
{

    [Subject(typeof(SagasConfiguration))]
    public class when_initializing_with_storage : given.a_sagas_configuration_and_container_object
    {
        static IEntityContextConfiguration entity_context_configuration;

        Establish context = () =>
                                {
                                    entity_context_configuration = new EntityContextConfiguration
                                                                        {
                                                                            Connection = new EntityContextConnection(),
                                                                            EntityContextType = typeof(EntityContext<>)
                                                                        };
                                    sagas_configuration.EntityContextConfiguration = entity_context_configuration;

                                };

        Because of = () => sagas_configuration.Initialize(container_mock.Object);

        It should_bind_the_specific_storage_for_sagas = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SagaHolder>), Moq.It.IsAny<Type>()));
        It should_bind_the_specific_storage_for_chapters = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<ChapterHolder>), Moq.It.IsAny<Type>()));

    }
}
