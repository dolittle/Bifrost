using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandValidators
{
    public class when_validating_and_there_are_two_providers : given.command_validators_with_two_providers
    {
        static Mock<ICommand>   command_mock;
        static CommandValidationResult result;

        Establish context = () => command_mock = new Mock<ICommand>();

        Because of = () => result = validators.Validate(command_mock.Object);

        It should_return_a_result = () => result.ShouldNotBeNull();

        It should_validate_using_the_first_validator = () => first_validator.validate_called.ShouldBeTrue();
        It should_hold_the_error_message_from_the_first_validator = () => result.CommandErrorMessages.ShouldContain(first_validator_command_error_message);
        It should_hold_the_validation_result_from_the_first_validator = () => result.ValidationResults.ShouldContain(first_validator_validation_result);

        It should_validate_using_the_second_validator = () => second_validator.validate_called.ShouldBeTrue();
        It should_hold_the_error_message_from_the_second_validator = () => result.CommandErrorMessages.ShouldContain(second_validator_command_error_message);
        It should_hold_the_validation_result_from_the_second_validator = () => result.ValidationResults.ShouldContain(second_validator_validation_result);
    }
}
