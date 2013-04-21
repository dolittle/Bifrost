using System;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_CommandInputValidator.given
{
    public class a_command_input_validator
    {
        protected static SimpleCommandInputValidator simple_command_input_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
                                {
                                    simple_command_input_validator = new SimpleCommandInputValidator();
                                    simple_command = new SimpleCommand(Guid.NewGuid());
                                };
    }
}