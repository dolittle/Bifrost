using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bifrost.Commands;
using Bifrost.FluentValidation.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandValidationService
{
    public class when_validating_a_command_that_has_model_rule_and_property_errors_on_input_validator : given.a_command_validation_service
    {
        const string ErrorMessage = "Something went wrong";
        const string AnotherErrorMessage = "Something else went wrong";

        static CommandValidationResult result;
        static Mock<ICommand>   command_mock;
        static Mock<ICommandInputValidator> command_input_validator_mock;

        Establish context = () =>
        {
            command_mock = new Mock<ICommand>();
            command_input_validator_mock = new Mock<ICommandInputValidator>();
            command_input_validator_mock.Setup(c => c.ValidateFor(command_mock.Object)).Returns(new[] {
                new ValidationResult(ErrorMessage,new[] { ModelRule<object>.ModelRulePropertyName }),
                new ValidationResult(AnotherErrorMessage, new[] { "SomeProperty" })
            });

            command_validator_provider_mock.Setup(c => c.GetInputValidatorFor(command_mock.Object)).Returns(command_input_validator_mock.Object);
        };

        Because of = () => result = command_validation_service.Validate(command_mock.Object);

        It should_have_one_command_error_message = () => result.CommandErrorMessages.Count().ShouldEqual(1);
        It should_have_the_correct_command_error_message = () => result.CommandErrorMessages.First().ShouldEqual(ErrorMessage);
        It should_have_one_validation_result = () => result.ValidationResults.Count().ShouldEqual(1);
        It should_have_the_correct_validation_result = () => result.ValidationResults.First().ErrorMessage.ShouldEqual(AnotherErrorMessage);
    }
}
