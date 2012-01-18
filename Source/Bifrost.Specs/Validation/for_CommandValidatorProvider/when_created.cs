using Bifrost.Specs.Validation.for_CommandValidatorProvider.given;
using Machine.Specifications;
using System;
using Bifrost.Execution;

namespace Bifrost.Specs.Validation.for_CommandValidatorProvider
{
    public class when_created : a_command_validator_provider_with_input_and_business_validators
    {
        It should_register_all_the_input_validators = () => command_validator_provider.RegisteredInputValidators.ShouldContainOnly(input_validators);
        It should_register_all_the_business_validators = () => command_validator_provider.RegisteredBusinessValidators.ShouldContainOnly(business_validators);
        It should_register_with_the_container_with_correct_lifecycle = () => container_mock.Verify(c => c.Bind(Moq.It.IsAny<Type>(), Moq.It.IsAny<Type>(), lifecycle), Moq.Times.Exactly(input_validators.Length+business_validators.Length));
    }
}