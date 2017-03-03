/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using FluentValidation;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationRuleExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="validator"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> DynamicValidationRule<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            IValidator validator,
            string name)
        {
#pragma warning disable 0618
            return ruleBuilder
                .NotNull()
                //.SetValidator(validator)
                .WithName(name);
#pragma warning restore 0618
        }
    }
}
