using System;
using Bifrost.Commands;
using Machine.Specifications;
using System.ComponentModel.DataAnnotations;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_containing_exception_and_one_validation_results
    {
        static CommandResult result;

        Because of = () => result = new CommandResult
        {
            Exception = new NotImplementedException(),
            ValidationResults = new [] {
                new ValidationResult("Something")
            }
        };

        It should_not_be_valid = () => result.Invalid.ShouldBeTrue();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
    }
}
