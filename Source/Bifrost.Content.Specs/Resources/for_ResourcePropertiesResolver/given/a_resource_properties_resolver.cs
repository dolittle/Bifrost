using System;
using Bifrost.Content.Resources;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Resources.for_ResourcePropertiesResolver.given
{
    public class a_resource_properties_resolver
    {
        protected static Mock<IContainer> container_mock;
        protected static ResourcePropertiesResolver resolver;
        protected static object resolved_instance;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    container_mock.Setup(c => c.Get(Moq.It.IsAny<Type>())).Returns((Type t) => resolved_instance = Activator.CreateInstance(t));
                                    resolver = new ResourcePropertiesResolver(container_mock.Object);
                                };
    }
}
