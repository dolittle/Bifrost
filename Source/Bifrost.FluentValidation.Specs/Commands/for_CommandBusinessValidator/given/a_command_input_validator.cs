using System;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandBusinessValidator.given
{
    public class a_command_business_validator
    {
        protected static ICanValidate simple_command_business_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
                                {
                                    simple_command_business_validator = new SimpleCommandBusinessValidator();
                                    simple_command = new SimpleCommand(Guid.NewGuid());
                                };
    }

    public class a_command_business_validator_with_ruleset
    {
        protected static ICanValidate simple_command_business_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
        {
            simple_command_business_validator = new SimpleCommandInputValidatorWithRuleset();
            simple_command = new SimpleCommand(Guid.NewGuid());
        };
    }
}