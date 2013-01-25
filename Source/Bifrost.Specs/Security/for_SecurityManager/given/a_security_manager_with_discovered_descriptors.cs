using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_SecurityManager.given
{
    public class a_security_manager_with_discovered_descriptors
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container;
        protected static SecurityManager security_manager;

        protected static Mock<ISecurityDescriptor> first_security_descriptor;
        protected static Mock<ISecurityDescriptor> second_security_descriptor;

        Establish context = () =>
        {
            first_security_descriptor = new Mock<ISecurityDescriptor>();
            second_security_descriptor = new Mock<ISecurityDescriptor>();

            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container = new Mock<IContainer>();
            type_discoverer_mock.Setup(d => d.FindMultiple<ISecurityDescriptor>())
                .Returns(new[] { typeof(ISecurityDescriptor), typeof(BaseSecurityDescriptor) });

            container.Setup(r => r.Get(typeof(ISecurityDescriptor))).Returns(first_security_descriptor);
            container.Setup(r => r.Get(typeof(BaseSecurityDescriptor))).Returns(second_security_descriptor);

            security_manager = new SecurityManager(type_discoverer_mock.Object, container.Object);
        };
    }
}
