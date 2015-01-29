#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Extensions;
using Bifrost.Validation;
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
        /// Defines a validation rule for a specify property.
        /// </summary>
        /// <example>
        /// RuleFor(x => x.Surname)...
        /// </example>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="expression">The expression representing the property to validate</param>
        /// <returns>an IRuleBuilder instance on which validators can be defined</returns>
        public IRuleBuilderInitial<T, TProperty> RuleForConcept<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if(expression == null)
                throw new ArgumentException("Cannot pass null to RuleForConcept");
            var rule = PropertyRule.Create<T, TProperty>(expression, (Func<CascadeMode>)(() => this.CascadeMode));
            rule.PropertyName = FromExpression(expression)??typeof(TProperty).Name;
            this.AddRule((IValidationRule)rule);
            return (IRuleBuilderInitial<T, TProperty>)new RuleBuilder<T, TProperty>(rule);
        }
        
        /// <summary>
        /// Validates the specified instance
        /// 
        /// </summary>
        /// <param name="instance">The object to validate</param>
        /// <returns>
        /// A ValidationResult object containing any validation failures
        /// </returns>
        public override ValidationResult Validate(T instance)
        {
            var result = base.Validate(instance);
            return BuildResult(result);
        }

        /// <summary>
        /// Validates the specified instance.
        /// 
        /// </summary>
        /// <param name="context">Validation Context</param>
        /// <returns>
        /// A ValidationResult object containing any validation failures.
        /// </returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
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

        static string FromExpression(LambdaExpression expression)
        {
            var stack = new Stack<string>();
            for (var memberExpression = ExpressionExtensions.Unwrap(expression.Body);
                    memberExpression != null;
                    memberExpression = ExpressionExtensions.Unwrap(memberExpression.Expression))
            {
                stack.Push(memberExpression.Member.Name);
            }
            return string.Join(".", stack.ToArray());
        }

        static string CleanPropertyName(string propertyName)
        {
            return propertyName.Replace(ModelRule<string>.ModelRulePropertyName, string.Empty).Trim('.');
        }
    }
}
