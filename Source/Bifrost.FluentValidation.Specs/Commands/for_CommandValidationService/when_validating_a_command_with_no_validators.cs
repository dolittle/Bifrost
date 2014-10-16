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
    public class when_validating_a_command_with_no_validators : a_command_validation_service
    {
        static CommandValidationResult result;
        static Mock<ICommand> command_mock;

        Establish context = () =>
                                {
                                    command_mock = new Mock<ICommand>();
                                    command_validator_provider_mock.Setup(cvs => cvs.GetInputValidatorFor(command_mock.Object)).Returns(() => new NullCommandInputValidator());
                                    command_validator_provider_mock.Setup(cvs => cvs.GetBusinessValidatorFor(command_mock.Object)).Returns(() => new NullCommandBusinessValidator());
                                };

        Because of = () => result = command_validation_service.Validate(command_mock.Object);

        It should_have_no_failed_validation_results = () => result.ValidationResults.ShouldBeEmpty();
    }
}
