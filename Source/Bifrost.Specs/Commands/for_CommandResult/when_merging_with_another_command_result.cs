using Bifrost.Commands;
using Bifrost.Validation;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Specs.Commands.for_CommandResult
{
    public class when_merging_with_another_command_result
    {
        static CommandResult   source;
        static CommandResult   target;
        static List<ValidationResult>  expected_combined_validation_results;
        static List<string> expected_combined_command_error_messages;

        Establish context = () =>
        {
            source = new CommandResult
            {
                ValidationResults = new ValidationResult[] {
                    new ValidationResult("Error 1 from source"),
                    new ValidationResult("Error 2 from source"),
                },
                CommandValidationMessages = new string[] {
                    "Command Error Message 1 from source",
                    "Command Error Message 2 from source",
                    "Command Error Message 3 from source"
                }
            };

            target = new CommandResult
            {
                ValidationResults = new ValidationResult[] {
                    new ValidationResult("Error 1 from target"),
                    new ValidationResult("Error 2 from target"),
                    new ValidationResult("Error 3 from target"),
                },
                CommandValidationMessages = new string[] {
                    "Command Error Message 1 from target",
                    "Command Error Message 2 from target",
                    "Command Error Message 3 from target",
                    "Command Error Message 4 from target"
                }
            };
            expected_combined_validation_results = new List<ValidationResult>();
            expected_combined_validation_results.AddRange(target.ValidationResults);
            expected_combined_validation_results.AddRange(source.ValidationResults);

            expected_combined_command_error_messages = new List<string>();
            expected_combined_command_error_messages.AddRange(target.CommandValidationMessages);
            expected_combined_command_error_messages.AddRange(source.CommandValidationMessages);

        };

        Because of = () => target.MergeWith(source);

        It should_have_validation_results_from_both = () => target.ValidationResults.ShouldContain(expected_combined_validation_results);
        It should_have_command_error_messages_from_both = () => target.CommandValidationMessages.ShouldContain(expected_combined_command_error_messages);
        It should_have_all_the_command_error_messages_and_validation_results = () =>
                                    {
                                        target.AllValidationMessages.ShouldContain(expected_combined_command_error_messages);
                                        target.AllValidationMessages.ShouldContain( expected_combined_validation_results.Select(vr => vr.ErrorMessage));
                                    };
                                           
    }
}
