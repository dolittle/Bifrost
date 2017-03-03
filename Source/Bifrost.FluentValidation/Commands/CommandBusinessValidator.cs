/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Validation;
using FluentValidation;
using FluentValidation.Internal;

namespace Bifrost.FluentValidation.Commands
{
    /// <summary>
    /// Base class to inherit from for basic business-rule validation of a command.
    /// </summary>
    /// <typeparam name="T">Concrete type of the Command to validate</typeparam>
    public abstract class CommandBusinessValidator<T> : BusinessValidator<T>, ICanValidate<T>, ICommandBusinessValidator where T : class, ICommand
    {
#pragma warning disable 1591 // Xml Comments
        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command as T);
            return BuildValidationResults(result);
        }

        public virtual IEnumerable<ValidationResult> ValidateFor(T command, string ruleSet)
        {
            var result = (this as IValidator<T>).Validate(command as T, ruleSet: ruleSet);
            return BuildValidationResults(result);
        }

        private static IEnumerable<ValidationResult> BuildValidationResults(global::FluentValidation.Results.ValidationResult result)
        {
            return from error in result.Errors
                select new ValidationResult(error.ErrorMessage, new[] {error.PropertyName});
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Add a predicate rule based on a Func that will be called when validation occurs
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T, TR}"/> that will be called for validation</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TR}"/> that can be used to fluently configure options for the rule</returns>
        public IRuleBuilderOptions<T, object> AddRule(Func<T, bool> validateFor)
        {
            var rule = CommandPredicateRule<T>.Create(validateFor);
            AddRule(rule);

            var ruleBuilder = new RuleBuilder<T, object>(rule);
            return ruleBuilder;
        }
    }
}