using System;
using System.Collections.Generic;
using Bifrost.Specs.Concepts.given;
using Bifrost.Validation;
using FluentValidation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ComposedCommandInputValidator.given
{
    public class a_composed_command_input_validator : commands
    {
        protected static ComposedCommandInputValidator<MySimpleCommand> composed_validator;

        protected static MySimpleCommand valid_command;
        protected static MySimpleCommand command_with_invalid_string;
        protected static MySimpleCommand command_with_invalid_long;
            
        Establish context = () =>
            {
                valid_command = new MySimpleCommand
                    {
                        LongConcept = 100,
                        StringConcept = "valid"
                    };

                command_with_invalid_string = new MySimpleCommand
                    {
                        LongConcept = 100
                    };

                command_with_invalid_long = new MySimpleCommand
                    {
                        StringConcept = "valid"
                    };

                var validators = new Dictionary<Type, IEnumerable<IValidator>>
                    {
                        {typeof (concepts.StringConcept), new[] {new StringConceptInputValidator()}},
                        {typeof (concepts.LongConcept), new[] {new LongConceptInputValidator()}}
                    };

                composed_validator = new ComposedCommandInputValidator<MySimpleCommand>(validators);
            };
    }
}