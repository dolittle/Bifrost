using Bifrost.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandValidationService.given
{
    public class a_command_validation_service
    {
        protected static Mock<ICommandValidatorProvider>    command_validator_provider_mock;
        protected static CommandValidationService   service;

        Establish context = () =>
        {
            command_validator_provider_mock = new Mock<ICommandValidatorProvider>();
            service = new CommandValidationService(command_validator_provider_mock.Object);
        };
    }
}
