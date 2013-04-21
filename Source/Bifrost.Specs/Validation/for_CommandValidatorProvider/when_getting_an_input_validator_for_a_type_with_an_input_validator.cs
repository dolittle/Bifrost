using System;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    public class when_getting_an_input_validator_for_a_type_with_an_input_validator : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate input_validator;

        Establish context = () => container_mock.Setup(c => c.Get(typeof(SimpleCommandInputValidator))).Returns(() => new SimpleCommandInputValidator());

        Because of = () => input_validator = command_validator_provider.GetInputValidatorFor(new SimpleCommand(Guid.NewGuid()));

        It should_return_the_correct_input_validator = () => input_validator.ShouldBeOfType(typeof(SimpleCommandInputValidator));
    }
}