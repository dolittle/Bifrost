using System.Linq;
using Machine.Specifications;
using Bifrost.Validation;
using Moq;
using Bifrost.Commands;
using System.ComponentModel.DataAnnotations;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_CommandValidationService
{
    public class when_validating_a_command_that_has_model_rule_error_on_input_validator : given.a_command_validation_service
    {
        const string ErrorMessage = "Something went wrong";

        static CommandValidationResult result;
        static Mock<ICommand>   command_mock;
        static Mock<ICanValidate> command_input_validator_mock;

        Establish context = () =>
        {
            command_mock = new Mock<ICommand>();
            command_input_validator_mock = new Mock<ICanValidate>();
            command_input_validator_mock.Setup(c => c.ValidateFor(command_mock.Object)).Returns(new[] {
                new ValidationResult(ErrorMessage,new[] { ModelRule<object>.ModelRulePropertyName })
            });

            command_validator_provider_mock.Setup(c => c.GetInputValidatorFor(command_mock.Object)).Returns(command_input_validator_mock.Object);
        };

        Because of = () => result = command_validation_service.Validate(command_mock.Object);

        It should_have_a_command_error_message = () => result.CommandErrorMessages.First().ShouldEqual(ErrorMessage);
    }
}
