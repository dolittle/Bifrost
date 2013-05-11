using FluentValidation.Results;
using Machine.Specifications;

namespace Bifrost.MSpec.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void ShouldBeValid(this ValidationResult validationResult)
        {
            validationResult.IsValid.ShouldBeTrue();
        }

        public static void ShouldBeInvalid(this ValidationResult validationResult)
        {
            validationResult.IsValid.ShouldBeFalse();
        }
    }
}
