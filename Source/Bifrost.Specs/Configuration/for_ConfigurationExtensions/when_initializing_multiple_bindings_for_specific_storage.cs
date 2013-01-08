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
using System.Collections;

namespace Bifrost.Specs.Configuration.for_ConfigurationExtensions
{
    [Subject(typeof(ConfigurationStorageElement))]
    public class when_initializing_multiple_bindings_for_specific_storage : given.a_configuration_element_with_storage
    {
        Because of = () => 
                        {
                            var listOfOperations = new Queue<bool>(new []{false, true, true, true, true, true});

                            container_mock.Setup(c => c.HasBindingFor(typeof(EntityContextConnection))).Returns(() => listOfOperations.Dequeue()); 
                            configuration.BindEntityContextTo<SomeType>(container_mock.Object);
                            configuration.BindEntityContextTo<SomeOtherType>(container_mock.Object); 
                        };

        It should_bind_the_specific_connection_only_once = () => container_mock.Verify(c => c.Bind(typeof(EntityContextConnection), connection), Times.Once());
        It should_bind_specific_storage_for_type = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SomeType>), typeof(EntityContext<SomeType>)), Times.Once());
        It should_bind_specific_storage_for_other_type = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<SomeOtherType>), typeof(EntityContext<SomeOtherType>)), Times.Once());

        It should_not_set_the_default_storage = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<>),Moq.It.IsAny<Type>()),Times.Never());

    }

}
