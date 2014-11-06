using Bifrost.FluentValidation.Commands;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandValidatorProvider
{
    [Subject(typeof(CommandValidatorProvider))]
    public class when_created : given.a_command_validator_provider_with_input_and_business_validators
    {
        It should_register_all_the_input_validators = () => command_validator_provider.RegisteredInputCommandValidators.ShouldContainOnly(command_input_validators);
        It should_register_all_the_business_validators = () => command_validator_provider.RegisteredBusinessCommandValidators.ShouldContainOnly(command_business_validators);
   }
}