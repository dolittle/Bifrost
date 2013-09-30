using System;
using System.Collections.Generic;
using Bifrost.Specs.Concepts.given;
using Bifrost.Validation;
using FluentValidation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ComposedCommandBusinessValidator.given
{
    public class a_composed_command_business_validator : commands
    {
        protected static ComposedCommandBusinessValidator<MySimpleCommand> composed_validator;

        protected static MySimpleCommand valid_command;
        protected static MySimpleCommand command_with_invalid_string;
        protected static MySimpleCommand command_with_invalid_long;
            
        Establish context = () =>
            {
                valid_command = new MySimpleCommand
                    {
                        LongConcept = 100,
                        StringConcept = "blah"
                    };

                command_with_invalid_string = new MySimpleCommand
                    {
                        LongConcept = 100
                    };

                command_with_invalid_long = new MySimpleCommand
                    {
                        StringConcept = "blah"
                    };

                var validators = new Dictionary<Type, IEnumerable<IValidator>>
                    {
                        {typeof (concepts.StringConcept), new[] {new StringConceptBusinessValidator()}},
                        {typeof (concepts.LongConcept), new[] {new LongConceptBusinessValidator()}}
                    };

                composed_validator = new ComposedCommandBusinessValidator<MySimpleCommand>(validators);
            };
    }
}