using Bifrost.FluentValidation.Commands;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_ComposedCommandBusinessValidator
{
    [Subject(typeof(ComposedCommandBusinessValidator<>))]
    public class when_composing_a_validator : given.a_composed_command_business_validator
    {
        It should_create_rules_for_each_type_validator_combination_passed_in = () => composed_validator.ShouldNotBeNull();
    }
}