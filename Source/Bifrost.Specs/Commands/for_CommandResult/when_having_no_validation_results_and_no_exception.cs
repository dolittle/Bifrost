using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_having_no_validation_results_and_no_exception
    {
        static CommandResult result;

        Because of = () => result = new CommandResult();

        It should_be_valid = () => result.Invalid.ShouldBeFalse();
        It should_be_successful = () => result.Success.ShouldBeTrue();
    }
}
