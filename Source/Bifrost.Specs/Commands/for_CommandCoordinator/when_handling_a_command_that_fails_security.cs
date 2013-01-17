using Bifrost.Commands;
using Machine.Specifications;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    public class when_handling_a_command_that_fails_security : given.a_command_coordinator
    {
        static ICommand command;
        static CommandResult result;

        Establish context = () => 
        {
            command = new SimpleCommand();
            command_security_manager_mock.Setup(c => c.CanHandle(Moq.It.IsAny<ICommand>())).Returns(false);
        };

        Because of = () => result = coordinator.Handle(command);

        It should_not_validate = () => command_validation_service_mock.Verify(c => c.Validate(command), Moq.Times.Never());
        It should_set_not_passed_in_command_result = () => result.PassedSecurity.ShouldBeFalse();
    }
}
