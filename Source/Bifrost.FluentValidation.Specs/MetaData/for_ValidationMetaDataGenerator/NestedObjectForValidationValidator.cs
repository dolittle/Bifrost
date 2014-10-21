using FluentValidation;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedObjectForValidationValidator : BusinessValidator<NestedObjectForValidation>
    {
        public NestedObjectForValidationValidator(ObjectForValidationValidator objectForValidationValidator)
        {
            RuleFor(n => n.SomeObject).SetValidator(objectForValidationValidator);
            RuleFor(n => n.FirstLevelString).NotEmpty().WithMessage("OMG WHY ARE YOU EMPTY??!?! NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!");
        }
    }
}