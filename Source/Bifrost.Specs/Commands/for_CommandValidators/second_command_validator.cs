using Bifrost.Commands;

namespace Bifrost.Specs.Commands.for_CommandValidators
{
    public class second_command_validator : ICommandValidator
    {
        public CommandValidationResult result_to_return;
        public bool validate_called;
        public ICommand command_passed_to_validate;

        public CommandValidationResult Validate(ICommand command)
        {
            validate_called = true;
            command_passed_to_validate = command;
            return result_to_return;
        }
    }
}
