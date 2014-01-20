using Bifrost.Read;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_FetchingSecurityManager.given
{
    public class a_fetching_security_manager
    {
        protected static Mock<ISecurityManager> security_manager_mock;
        protected static FetchingSecurityManager fetching_security_manager;

        Establish context = () =>
        {
            security_manager_mock = new Mock<ISecurityManager>();
            fetching_security_manager = new FetchingSecurityManager(security_manager_mock.Object);
        };
    }
}
