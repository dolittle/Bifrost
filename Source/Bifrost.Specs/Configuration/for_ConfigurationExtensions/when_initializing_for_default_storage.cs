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
    public class when_initializing_for_default_storage : given.a_configuration_element_with_storage
    {
        static Type default_type;
        Establish context = () =>
        {
            default_type = typeof(EntityContext<>);
        };


        Because of = () => configuration.BindDefaultEntityContext(container_mock.Object);

        It should_bind_the_default_connection_connection = () => container_mock.Verify(c => c.Bind(typeof(EntityContextConnection), connection));
        It should_bind_to_the_default_storage_type = () => container_mock.Verify(c => c.Bind(typeof(IEntityContext<>),default_type));
        It should_not_make_any_other_calls_on_the_container = () => container_mock.VerifyAll();
      
    }
}
