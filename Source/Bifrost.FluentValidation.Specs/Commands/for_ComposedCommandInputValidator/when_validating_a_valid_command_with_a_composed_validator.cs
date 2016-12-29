using System.Collections.Generic;
using Bifrost.FluentValidation.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_ComposedCommandInputValidator
{
    [Subject(typeof(ComposedCommandInputValidator<>))]
    public class when_validating_a_valid_command_with_a_composed_validator : given.a_composed_command_input_validator
    {
        static IEnumerable<ValidationResult> result;

        Because of = () => result = composed_validator.ValidateFor(valid_command);

        It should_be_valid = () => result.ShouldBeEmpty();
    }
}