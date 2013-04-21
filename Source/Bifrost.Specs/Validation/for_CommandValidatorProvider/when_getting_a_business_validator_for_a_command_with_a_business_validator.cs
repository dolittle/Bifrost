using System;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    public class when_getting_a_business_validator_for_a_command_with_a_business_validator : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate business_validator;

        Establish context = () => container_mock.Setup(c => c.Get(typeof(SimpleCommandBusinessValidator))).Returns(() => new SimpleCommandBusinessValidator());

        Because of = () => business_validator = command_validator_provider.GetBusinessValidatorFor(new SimpleCommand(Guid.NewGuid()));

        It should_return_the_correct_business_validator = () => business_validator.ShouldBeOfType(typeof(SimpleCommandBusinessValidator));
    }
}