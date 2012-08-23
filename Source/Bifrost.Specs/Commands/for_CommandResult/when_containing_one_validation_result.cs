using Machine.Specifications;
using Bifrost.Commands;
using System.ComponentModel.DataAnnotations;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_containing_one_validation_result
    {
        static CommandResult    result;

        Because of = () => result = new CommandResult {
            ValidationResults = new [] {
                new ValidationResult("Something")
            }
        };

        It should_not_be_valid = () => result.Invalid.ShouldBeTrue();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
    }
}
