using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Bifrost.Specs.Validation.for_CommandValidationService.given;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_CommandValidationService
{
    public class when_validating_a_command_that_has_incorrect_inputs : a_command_validation_service
    {
        static IEnumerable<ValidationResult> input_validation_errors;
        static CommandValidationResult result;
        static Mock<ICommand> command_mock;
        static Mock<ICanValidate> command_input_validator_mock;
        static Mock<ICanValidate> command_validator_mock;

        Establish context = () =>
        {
            input_validation_errors = new List<ValidationResult>()
                                          {
                                              new ValidationResult("first failed input", new[] { "AProperty" }),
                                              new ValidationResult("second failed input", new[] { "AnotherProperty" })
                                          };

            command_mock = new Mock<ICommand>();
            command_input_validator_mock = new Mock<ICanValidate>();
            command_validator_mock = new Mock<ICanValidate>();

            command_input_validator_mock.Setup(iv => iv.ValidateFor(command_mock.Object)).Returns(input_validation_errors);

            command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_mock.Object)).Returns(command_input_validator_mock.Object);
        };

        Because of = () => result = command_validation_service.Validate(command_mock.Object);

        It should_have_failed_validations = () => result.ValidationResults.ShouldNotBeEmpty();
        It should_have_all_the_failed_input_validations = () => result.ValidationResults.ShouldContainOnly(input_validation_errors);
        It should_not_have_validated_the_command_business_rules = () => command_validator_mock.VerifyAll();
    }
}