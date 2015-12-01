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
using System.Linq.Expressions;
using Bifrost.Extensions;
using FluentValidation;
using FluentValidation.Internal;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Validation extensions for building validation rules
    /// </summary>
    public static class AbstractValidatorExtensions
    {
        /// <summary>
        /// Defines a concept validation rule for a specify property.
        /// </summary>
        /// <typeparam name="T">Type to add validation rules for</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="validator">The validator to apply the validation rule to.</param>
        /// <param name="expression">The expression representing the property to validate</param>
        /// <returns>an IRuleBuilder instance on which validators can be defined</returns>
        public static IRuleBuilderInitial<T, TProperty> AddRuleForConcept<T, TProperty>(
            this AbstractValidator<T> validator,
            Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentException("Cannot pass null to RuleForConcept");
            var rule = PropertyRule.Create(expression, () => validator.CascadeMode);
            rule.PropertyName = FromExpression(expression) ?? typeof(TProperty).Name;
            validator.AddRule(rule);
            return new RuleBuilder<T, TProperty>(rule);
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
    }
}
