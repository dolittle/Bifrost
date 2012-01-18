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
    public class when_validating_a_command_that_has_correct_inputs_but_does_not_fulfill_business_rules : a_command_validation_service
    {
        static IEnumerable<ValidationResult> business_validation_errors;
        static IEnumerable<ValidationResult> validation_results;
        static Mock<ICommand> command_mock;
        static Mock<ICommandInputValidator> command_input_validator_mock;
        static Mock<ICommandBusinessValidator> command_business_validator_mock;

        Establish context = () =>
        {
            business_validation_errors = new List<ValidationResult>()
                                          {
                                              new ValidationResult("first failed input"),
                                              new ValidationResult("second failed input")
                                          };

            command_mock = new Mock<ICommand>();
            command_input_validator_mock = new Mock<ICommandInputValidator>();
            command_business_validator_mock = new Mock<ICommandBusinessValidator>();

            command_input_validator_mock.Setup(iv => iv.ValidateInput(command_mock.Object)).Returns(new List<ValidationResult>());
            command_business_validator_mock.Setup(cv => cv.Validate(command_mock.Object)).Returns(business_validation_errors);

            command_validator_service_mock.Setup(cvs => cvs.GetInputValidatorFor(command_mock.Object)).Returns(command_input_validator_mock.Object);
            command_validator_service_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_mock.Object)).Returns(command_business_validator_mock.Object);
        };

        Because of = () => validation_results = command_validation_service.Validate(command_mock.Object);

        It should_have_failed_validations = () => validation_results.ShouldNotBeEmpty();
        It should_have_all_the_failed_business_rule_validations = () => validation_results.ShouldContainOnly(business_validation_errors);
        It should_have_validated_the_command_inputs = () => command_input_validator_mock.VerifyAll();
    }
}