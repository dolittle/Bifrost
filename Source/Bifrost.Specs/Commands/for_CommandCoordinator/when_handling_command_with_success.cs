using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_command_with_success : given.a_command_coordinator
    {
        static CommandResult result;

        Establish context = () =>
            command_validators_mock.Setup(cvs => cvs.Validate(command)).Returns(new CommandValidationResult());

        Because of = () => result = coordinator.Handle(command);

        It should_have_validated_the_command = () => command_validators_mock.VerifyAll();
        It should_have_a_result = () => result.ShouldNotBeNull();
        It should_have_success_set_to_true = () => result.Success.ShouldBeTrue();
    }
}