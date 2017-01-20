using Bifrost.FluentValidation.Commands;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_a_business_validator_for_a_command_with_a_business_validator : given.a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate business_validator;

        Establish context = () => container_mock.Setup(c => c.Get(typeof(SimpleCommandBusinessValidator))).Returns(() => new SimpleCommandBusinessValidator());

        Because of = () => business_validator = command_validator_provider.GetBusinessValidatorFor(new SimpleCommand());

        It should_return_the_correct_business_validator = () => business_validator.ShouldBeOfExactType<SimpleCommandBusinessValidator>();
    }
}