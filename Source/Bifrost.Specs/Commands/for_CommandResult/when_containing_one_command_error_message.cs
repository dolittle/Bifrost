using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_containing_one_command_error_message 
    {
        static CommandResult result;

        Because of = () => result = new CommandResult
        {
            CommandErrorMessages = new string[] { "Something went wrong" }
        };

        It should_be_valid = () => result.Invalid.ShouldBeFalse();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
    }
}
