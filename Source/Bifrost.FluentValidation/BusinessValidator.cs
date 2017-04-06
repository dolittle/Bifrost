/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Base class to inherit from for basic business validation rules
    /// </summary>
    /// <typeparam name="T">Type to add validation rules for</typeparam>
    public class BusinessValidator<T> : AbstractValidator<T>, IValidateBusinessRules<T>
    {
        /// <summary>
        /// Start building rules for the model
        /// </summary>
        /// <returns><see cref="IRuleBuilderInitial{T, T}"/> that can be used to fluently set up rules</returns>
        public IRuleBuilderInitial<T, T> ModelRule()
        {
            var rule = new ModelRule<T>();
            AddRule(rule);
            var builder = new RuleBuilder<T, T>(rule);
            return builder;
        }

        /// <summary>
        /// Defines a concept validation rule for a specify property.
        /// </summary>
        /// <example>
        /// RuleForConcept(x => x.Surname)...
        /// </example>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="expression">The expression representing the property to validate</param>
        /// <returns>an IRuleBuilder instance on which validators can be defined</returns>
        public IRuleBuilderInitial<T, TProperty> RuleForConcept<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return this.AddRuleForConcept(expression);
        }
        

        
        
        /*public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context.InstanceToValidate);
            return BuildResult(result);
        }*/

        

        /// <summary>
        /// Validates the provided instance using the specified ruleset
        /// </summary>
        /// <param name="instance">TThe object to be validated</param>
        /// <param name="ruleSet">A comma separated list of rulesets to be used when validating</param>
        /// <returns>A ValidationResult object containing any validation failures</returns>
        public ValidationResult Validate(T instance, string ruleSet)
        {
            var result = (this as IValidator<T>).Validate(instance, ruleSet: ruleSet);
            return BuildResult(result);
        }

        static ValidationResult BuildResult(ValidationResult rawResult)
        {
            var cleanedErrors = rawResult.Errors.Select(error => new ValidationFailure(CleanPropertyName(error.PropertyName), error.ErrorMessage, error.AttemptedValue)
                {
                    CustomState = error.CustomState
                }).ToList();

            return new ValidationResult(cleanedErrors);
        }

        static string CleanPropertyName(string propertyName)
        {
            return propertyName.Replace(ModelRule<string>.ModelRulePropertyName, string.Empty).Trim('.');
        }
    }
}
