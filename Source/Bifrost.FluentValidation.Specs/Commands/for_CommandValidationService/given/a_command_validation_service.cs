using Bifrost.Commands;
using Bifrost.FluentValidation.Commands;
using Bifrost.FluentValidation.Specs.Commands.for_CommandValidatorProvider.given;
using Machine.Specifications;
using Moq;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandValidationService.given
{
    public class a_command_validation_service : a_command_validator_provider_with_input_and_business_validators
    {
        protected static ICommandValidator command_validation_service;
        protected static Mock<ICommandValidatorProvider> command_validator_provider_mock;

        Establish context = () =>
                                {
                                    command_validator_provider_mock = new Mock<ICommandValidatorProvider>();
                                    command_validation_service = new CommandValidator(command_validator_provider_mock.Object);
                                };
    }
}