using System;
using Bifrost.Resources;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Resources.for_ResourcePropertiesResolver.given
{
    public class a_resource_properties_resolver
    {
        protected static Mock<IServiceLocator> ServiceLocatorMock;
        protected static ResourcePropertiesResolver Resolver;
        protected static object ResolvedInstance;

        Establish context = () =>
                                {
                                    ServiceLocatorMock = new Mock<IServiceLocator>();
                                    ServiceLocatorMock.Setup(s => s.GetInstance(Moq.It.IsAny<Type>())).Returns((Type t) => ResolvedInstance = Activator.CreateInstance(t));
                                    Resolver = new ResourcePropertiesResolver(ServiceLocatorMock.Object);
                                };
    }
}
