using System;
using Bifrost.Specs.Resources.for_Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Resources.for_ResourcePropertiesResolver
{
    [Subject(Subjects.resolving)]
    public class when_resolving_properties_not_implemented_as_resource : given.a_resource_properties_resolver
    {
        Because of = () => Resolver.ResolvePropertiesFor(new ClassWithRegularProperties());

        It should_ignore_properties = () => ServiceLocatorMock.Verify(s => s.GetInstance(Moq.It.IsAny<Type>()), Times.Never());
    }
}
