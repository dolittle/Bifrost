using Bifrost.Commands;
using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    public class when_getting_a_business_validator_for_a_command_with_no_business_validator : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate business_validator;
        static Mock<ICommand> command_mock;

        Establish context = () =>
        {
            command_mock = new Mock<ICommand>();
        };

        Because of = () => business_validator = command_validator_provider.GetBusinessValidatorFor(command_mock.Object);

        It should_return_the_correct_input_validator = () => business_validator.ShouldBeOfType(typeof(NullCommandBusinessValidator));
    }
}