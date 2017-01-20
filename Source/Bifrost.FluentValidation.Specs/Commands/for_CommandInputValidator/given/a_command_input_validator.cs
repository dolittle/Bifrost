using Bifrost.Testing.Fakes.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Commands.for_CommandInputValidator.given
{
    public class a_command_input_validator
    {
        protected static SimpleCommandInputValidator simple_command_input_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
                                {
                                    simple_command_input_validator = new SimpleCommandInputValidator();
                                    simple_command = new SimpleCommand();
                                };
    }

    public class a_command_input_validator_with_ruleset
    {
        protected static ICanValidate simple_command_input_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
        {
            simple_command_input_validator = new SimpleCommandInputValidatorWithRuleset();
            simple_command = new SimpleCommand();
        };
    }
}