using System;
using Bifrost.Configuration;
using Bifrost.Entities;
using Bifrost.Fakes.Entities;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Configuration.for_ConfigurationExtensions
{
    [Subject(typeof(ConfigurationStorageElement))]
    public class when_initializing_for_specific_storage : given.a_configuration_element_with_storage
    {
        Because of = () => configuration.BindEntityContextTo<SomeType>(container_mock.Object);

        It should_bind_the_specific_connection = () => container_mock.Verify(c => c.Bind(typeof(EntityContextConnection), connection));
        It should_bind_specific_storage_for_type = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SomeType>), typeof(EntityContext<SomeType>)));
        It should_not_set_the_default_storage = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<>),Moq.It.IsAny<Type>()),Times.Never());
    }

}
