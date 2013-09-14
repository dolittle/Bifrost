using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_getting_an_input_validator_for_a_type_with_no_input_validator_for_the_second_time : a_command_validator_provider_with_input_and_business_validators
    {
        static ICanValidate input_validator;
        static MySimpleCommand command;

        Establish context = () =>
            {
                command  = new MySimpleCommand();
            };

        Because of = () => input_validator = command_validator_provider.GetInputValidatorFor(command);

        It should_return_the_a_dynamically_constructed_validator = () => input_validator.ShouldBeOfType(typeof(ComposedCommandInputValidator<MySimpleCommand>));
    }
}