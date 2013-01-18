using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_SecurityManager.given
{
    public class a_security_manager
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static SecurityManager security_manager;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            security_manager = new SecurityManager(type_discoverer_mock.Object);
        };
    }
}
