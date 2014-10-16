using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bifrost.Specs.Validation.for_CommandInputValidator.given;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandInputValidator
{
    public class when_validating_an_invalid_command : a_command_input_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_input_validator.ValidateFor(simple_command);

        It should_have_invalid_properties = () => results.Count().ShouldEqual(2);
    }
}