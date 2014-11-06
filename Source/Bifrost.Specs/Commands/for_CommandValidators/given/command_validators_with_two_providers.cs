using Bifrost.Commands;
using System.Linq;
using Bifrost.Validation;
using Machine.Specifications;
using System.ComponentModel.DataAnnotations;

namespace Bifrost.Specs.Commands.for_CommandValidators.given
{
    public class command_validators_with_two_providers : all_dependencies
    {
        protected static CommandValidators validators;

        protected static first_command_validator first_validator;
        protected static CommandValidationResult first_validator_result;
        protected const string first_validator_command_error_message = "first validator error";
        protected const string first_validator_validation_message = "first validator validation message";
        protected static ValidationResult first_validator_validation_result;

        protected static second_command_validator second_validator;
        protected static CommandValidationResult second_validator_result;
        protected const string second_validator_command_error_message = "second validator error";
        protected const string second_validator_validation_message = "second validator validation message";
        protected static ValidationResult second_validator_validation_result;

        Establish context = () =>
        {
            first_validator = new first_command_validator();
            first_validator_validation_result = new ValidationResult(first_validator_validation_message);
            first_validator_result = new CommandValidationResult
            {
                CommandErrorMessages = new string[] { first_validator_command_error_message },
                ValidationResults = new[] { first_validator_validation_result }
            };
            first_validator.result_to_return = first_validator_result;

            second_validator = new second_command_validator();
            second_validator_validation_result = new ValidationResult(second_validator_validation_message);
            second_validator_result = new CommandValidationResult
            {
                CommandErrorMessages = new string[] { second_validator_command_error_message },
                ValidationResults = new[] { second_validator_validation_result }
            };
            second_validator.result_to_return = second_validator_result;

            validators_mock.Setup(v => v.GetEnumerator()).Returns(new ICommandValidator[] {
                first_validator,
                second_validator
            }.ToList<ICommandValidator>().GetEnumerator());

            validators = new CommandValidators(validators_mock.Object);
        };
    }
}
