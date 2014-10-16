using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Specs.Validation.for_CommandInputValidator.given;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_CommandInputValidator
{
    public class when_validating_a_valid_command : a_command_input_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
                                {
                                    simple_command.SomeString = "Something, something, something, Dark Side";
                                    simple_command.SomeInt = 42;
                                };

        Because of = () => results = simple_command_input_validator.ValidateFor(simple_command);

        It should_have_no_invalid_properties = () => results.ShouldBeEmpty();
    }
}