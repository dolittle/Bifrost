using Bifrost.Commands;
using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    public class when_getting_an_input_validator_for_a_type_with_no_input_validator : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate input_validator;
        static Mock<ICommand> command_mock;

        Establish context = () =>
                                {
                                    command_mock = new Mock<ICommand>();
                                };

        Because of = () => input_validator = command_validator_provider.GetInputValidatorFor(command_mock.Object);

        It should_return_the_correct_input_validator = () => input_validator.ShouldBeOfType(typeof(NullCommandInputValidator));
    }
}