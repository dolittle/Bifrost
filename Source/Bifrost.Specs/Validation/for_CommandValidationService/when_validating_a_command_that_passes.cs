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
    public class when_validating_a_command_that_passes : a_command_validation_service
    {
        static IEnumerable<ValidationResult> validation_results;
        static Mock<ICommand> command_mock;
        static Mock<ICommandInputValidator> command_input_validator_mock;
        static Mock<ICommandBusinessValidator> command_validator_mock;

        Establish context = () =>
        {
            command_mock = new Mock<ICommand>();
            command_input_validator_mock = new Mock<ICommandInputValidator>();
            command_validator_mock = new Mock<ICommandBusinessValidator>();

            command_input_validator_mock.Setup(iv => iv.ValidateInput(command_mock.Object)).Returns(new List<ValidationResult>());
            command_validator_mock.Setup(cv => cv.Validate(command_mock.Object)).Returns(new List<ValidationResult>());

            command_validator_service_mock.Setup(cvs => cvs.GetInputValidatorFor(command_mock.Object)).Returns(command_input_validator_mock.Object);
            command_validator_service_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_mock.Object)).Returns(command_validator_mock.Object);
        };

        Because of = () => validation_results = command_validation_service.Validate(command_mock.Object);

        It should_have_no_failed_validation_results = () => validation_results.ShouldBeEmpty();
        It should_have_validated_the_command_inputs = () => command_input_validator_mock.VerifyAll();
        It should_have_validated_the_command_business_rules = () => command_validator_mock.VerifyAll();
    }
}