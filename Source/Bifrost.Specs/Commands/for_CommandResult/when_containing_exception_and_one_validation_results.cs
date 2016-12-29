using System;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_containing_exception_and_one_validation_results
    {
        static CommandResult result;
        static string error_message = "Something";

        Because of = () => result = new CommandResult
        {
            Exception = new NotImplementedException(),
            ValidationResults = new [] {
                new ValidationResult("Something")
            }
        };

        It should_not_be_valid = () => result.Invalid.ShouldBeTrue();
        It should_not_be_successful = () => result.Success.ShouldBeFalse();
        It should_have_only_the_validation_result_in_all_validation_errors = () =>
                                                                                    {
                                                                                        result.AllValidationMessages.Count().ShouldEqual(1);
                                                                                        result.AllValidationMessages.First().ShouldEqual(error_message);
                                                                                    };
    }
}
