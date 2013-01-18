using Bifrost.Commands;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandSecurityManager.given
{
    public class a_command_security_manager
    {
        protected static Mock<ISecurityManager> security_manager_mock;
        protected static CommandSecurityManager command_security_manager;

        Establish context = () =>
        {
            security_manager_mock = new Mock<ISecurityManager>();
            command_security_manager = new CommandSecurityManager(security_manager_mock.Object);
        };
    }
}
