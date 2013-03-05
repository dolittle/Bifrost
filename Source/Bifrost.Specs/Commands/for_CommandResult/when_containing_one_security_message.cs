using System.Linq;
using Machine.Specifications;
using Bifrost.Commands;
using System.ComponentModel.DataAnnotations;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_containing_one_security_message
    {
        static CommandResult result;
        static string error_message = "Something";

        Because of = () => result = new CommandResult {
            SecurityMessages = new [] {
                error_message
            }
        };

        It should_be_valid = () => result.Invalid.ShouldBeFalse();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
        It should_not_pass_security = () => result.PassedSecurity.ShouldBeFalse();
    }
}
