using Bifrost.FluentValidation.Commands;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandForValidationValidator : CommandInputValidator<CommandForValidation>
    {
        public const string NotEmptyErrorMessage = "Should not be empty";
        public const string EmailAddressErrorMessage = "Not a valid email";

        public const int LessThanValue = 50;
        public const int GreaterThanValue = 5;

        public const string LessThanErrorMessage = "Should be less than";
        public const string GreaterThanErrorMessage = "Should be greater than";

        public CommandForValidationValidator()
        {
            RuleFor(o => o.SomeString)
                .NotEmpty()
                    .WithMessage(NotEmptyErrorMessage)
                .EmailAddress()
                    .WithMessage(EmailAddressErrorMessage);

            RuleFor(o => o.SomeInt)
                .LessThan(LessThanValue)
                    .WithMessage(LessThanErrorMessage)
                .GreaterThan(GreaterThanValue)
                    .WithMessage(GreaterThanErrorMessage);
        }
    }
}
