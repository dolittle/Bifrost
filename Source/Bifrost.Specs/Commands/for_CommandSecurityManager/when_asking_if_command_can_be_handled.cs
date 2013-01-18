using Bifrost.Commands;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandSecurityManager
{
    public class when_asking_if_command_can_be_handled : given.a_command_security_manager
    {
        static ICommand command;

        Establish context = () => command = new SimpleCommand();

        Because of = () => command_security_manager.CanHandle(command);

        It should_delegate_the_request_for_security_to_the_security_manager = () => security_manager_mock.Verify(s => s.HasAccess(command), Moq.Times.Once());
    }
}
