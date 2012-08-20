using Machine.Specifications;
using Bifrost.Commands;
using Bifrost.Validation;

namespace Bifrost.Specs.Commands.for_CommandValidationService
{
    public class when_validating_without_any_errors : given.a_command_validation_service
    {
        static Command  command;
        static CommandValidationResult result;

        Establish context = () =>
        {
            command = new Command();
        };

        Because of = () => result = service.Validate(command);

        It should_return_result = () => result.ShouldNotBeNull();
    }
}
