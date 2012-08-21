using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Validation
{
    public static class FluentValidationModelValidatorProviderExtensions
    {
        public static void AddPropertyValidators(this FluentValidationModelValidatorProvider provider)
        {
            provider.Add(typeof(GreaterThanValidator),
                (metadata, context, rule, validator) => new GreaterThanPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(GreaterThanOrEqualValidator),
                (metadata, context, rule, validator) => new GreaterThanOrEqualPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(LessThanValidator),
                (metadata, context, rule, validator) => new LessThanPropertyValidator(metadata, context, rule, validator));
            provider.Add(typeof(LessThanOrEqualValidator),
                (metadata, context, rule, validator) => new LessThanPropertyValidator(metadata, context, rule, validator));
        }
    }
}
