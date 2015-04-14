using FluentValidation;
using FluentValidation.Results;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Base class to inherit from for basic input validation rules
    /// </summary>
    /// <typeparam name="T">Type to add validation rules for</typeparam>
    public class InputValidator<T> : AbstractValidator<T>, IValidateInput<T>
    {
        /// <summary>
        /// Validates the provided instance using the specified ruleset
        /// </summary>
        /// <param name="instance">TThe object to be validated</param>
        /// <param name="ruleSet">A comma separated list of rulesets to be used when validating</param>
        /// <returns>A ValidationResult object containing any validation failures</returns>
        public ValidationResult Validate(T instance, string ruleSet)
        {
            var result = (this as IValidator<T>).Validate(instance, ruleSet: ruleSet);
            return result;
        }
    }
}