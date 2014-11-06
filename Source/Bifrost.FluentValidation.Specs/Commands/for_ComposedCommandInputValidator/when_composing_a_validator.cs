using Bifrost.FluentValidation.Commands;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_ComposedCommandInputValidator
{
    [Subject(typeof (ComposedCommandInputValidator<>))]
    public class when_composing_a_validator : given.a_composed_command_input_validator
    {
        It should_create_rules_for_each_type_validator_combination_passed_in = () => composed_validator.ShouldNotBeNull();
    }
}