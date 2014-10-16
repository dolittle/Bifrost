using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Validation.for_CommandValidationService.given
{
    public class a_command_validation_service : a_command_validator_provider_with_input_and_business_validators
    {
        protected static ICommandValidationService command_validation_service;
        protected static Mock<ICommandValidatorProvider> command_validator_provider_mock;

        Establish context = () =>
                                {
                                    command_validator_provider_mock = new Mock<ICommandValidatorProvider>();
                                    command_validation_service = new CommandValidationService(command_validator_provider_mock.Object);
                                };
    }
}