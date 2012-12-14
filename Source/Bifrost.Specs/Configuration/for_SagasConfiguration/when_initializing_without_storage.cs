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
using Bifrost.Sagas;

namespace Bifrost.Specs.Configuration.for_SagasConfiguration
{
    [Subject(typeof(SagasConfiguration))]
    public class when_initializing_without_storage : given.a_sagas_configuration_and_container_object
    {
        
        Because of = () => sagas_configuration.Initialize(container_mock.Object);

        It should_be_initialized = () => sagas_configuration.ShouldNotBeNull();
        It should_not_set_up_storage_for_sagas = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SagaHolder>), Moq.It.IsAny<Type>()), Times.Never());
        It should_not_set_up_storage_for_chapters = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<ChapterHolder>), Moq.It.IsAny<Type>()), Times.Never());
    }
}
