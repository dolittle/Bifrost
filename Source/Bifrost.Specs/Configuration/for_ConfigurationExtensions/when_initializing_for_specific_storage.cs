using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Bifrost.Configuration;
using Moq;
using Bifrost.Execution;
using Bifrost.Entities;
using Bifrost.Fakes.Entities;

namespace Bifrost.Specs.Configuration.for_ConfigurationExtensions
{
    [Subject(typeof(ConfigurationStorageElement))]
    public class when_initializing_for_specific_storage : given.a_configuration_element_with_storage
    {
        static Type default_type;
        Establish context = () =>
        {
            default_type = typeof(EntityContext<>);
        };


        Because of = () => configuration.BindEntityContextTo<SomeType>(container_mock.Object);

        It should_bind_the_specific_connection = () => container_mock.Verify(c => c.Bind(typeof(EntityContextConnection), connection));
        It should_bind_specific_storage_for_type = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SomeType>), default_type));
        It should_not_set_the_default_storage = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<>),Moq.It.IsAny<Type>()),Times.Never());
    }

}
