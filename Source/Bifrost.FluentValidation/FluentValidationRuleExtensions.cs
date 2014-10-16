using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationRuleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="validator"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IAmValidatable> DynamicValidationRule<T>(this IRuleBuilder<T, IAmValidatable> ruleBuilder, IValidator validator, string name)
        {
#pragma warning disable 0618
            return ruleBuilder
                .NotNull()
                .SetValidator(validator)
                .WithName(name);
#pragma warning restore 0618
        }
    }
}