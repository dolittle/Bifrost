using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ComposedCommandBusinessValidator
{
    [Subject(typeof(ComposedCommandBusinessValidator<>))]
    public class when_composing_a_validator : for_ComposedCommandInputValidator.given.a_composed_command_input_validator
    {
        It should_create_rules_for_each_type_validator_combination_passed_in = () => composed_validator.ShouldNotBeNull();
    }
}