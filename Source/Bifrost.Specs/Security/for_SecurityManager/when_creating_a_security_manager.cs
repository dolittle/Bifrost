using System;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityManager
{
    [Subject(typeof(SecurityManager))]
    public class when_creating_a_security_manager : given.a_security_manager_with_discovered_descriptors
    {
        It should_discover_all_security_descriptors = () => type_discoverer_mock.Verify(td => td.FindMultiple<ISecurityDescriptor>(), Times.Once());
        It should_create_instances_of_each_discovered_descriptor = () => container.Verify(c => c.Get(Moq.It.IsAny<Type>()), Times.Exactly(2));
    }
}