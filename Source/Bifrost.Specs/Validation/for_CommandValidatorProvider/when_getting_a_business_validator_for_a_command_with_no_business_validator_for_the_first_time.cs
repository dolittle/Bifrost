using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_a_business_validator_for_a_command_with_no_business_validator_for_the_first_time : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate business_validator;
        static MySimpleCommand command;

        Establish context = () =>
        {
            command = new MySimpleCommand();
        };

        Because of = () => business_validator = command_validator_provider.GetBusinessValidatorFor(command);

        It should_return_a_dynamically_constructed_validator = () => business_validator.ShouldBeOfType(typeof(ComposedCommandBusinessValidator<MySimpleCommand>));
    }
}