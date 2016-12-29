using System.Collections.Generic;
using Bifrost.FluentValidation.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandInputValidator
{
    [Subject(typeof(CommandInputValidator<>))]
    public class when_validating_a_valid_command : given.a_command_input_validator
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