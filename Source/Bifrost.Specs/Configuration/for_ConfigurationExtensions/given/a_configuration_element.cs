using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Moq;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Fakes.Configuration;
using Bifrost.Entities;
using Bifrost.Fakes.Entities;

namespace Bifrost.Specs.Configuration.for_ConfigurationExtensions.given
{

    public class a_configuration_element_with_storage
    {
        protected static Mock<IContainer> container_mock;
        protected static IEntityContextConfiguration configuration;
        protected static IEntityContextConnection connection;

        Establish context = () =>
        {
            container_mock = new Mock<IContainer>();
            connection = new EntityContextConnection();
            configuration = new EntityContextConfiguration { Connection = connection, EntityContextType = typeof(EntityContext<>)};
        };
    }
}
