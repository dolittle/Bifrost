using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedObjectForValidationValidator : AbstractValidator<NestedObjectForValidation>
    {
        public NestedObjectForValidationValidator(ObjectForValidationValidator objectForValidationValidator)
        {
            RuleFor(n => n.SomeObject).SetValidator(objectForValidationValidator);
            RuleFor(n => n.FirstLevelString).NotEmpty().WithMessage("OMG WHY ARE YOU EMPTY??!?! NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!");
        }
    }
}