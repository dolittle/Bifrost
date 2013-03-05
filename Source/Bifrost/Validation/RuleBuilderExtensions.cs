#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;

namespace Bifrost.Validation
{
    /// <summary>
    /// Validation extensions for building validation rules
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="builder"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WithDynamicStateFrom<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, Expression<Func<T, object>> expression)
        {
            ThrowIfNotCorrectRuleBuilder(builder);
            ThrowIfNotCorrectValidator(builder);

            var validator = ((RuleBuilder<T, TProperty>)builder).Rule.CurrentValidator as PropertyValidatorWithDynamicState;
            validator.AddExpression(expression);
            return builder;
        }

        static void ThrowIfNotCorrectValidator<T, TProperty>(IRuleBuilderOptions<T, TProperty> builder)
        {
            var actualBuilder = builder as RuleBuilder<T, TProperty>;
            if (!(actualBuilder.Rule.CurrentValidator is PropertyValidatorWithDynamicState))
                throw new InvalidValidatorTypeException(
                    string.Format("Dynamic state is only supported on a property validator that inherits from {0}",
                        typeof(PropertyValidatorWithDynamicState))
                );
               
        }

        static void ThrowIfNotCorrectRuleBuilder<T, TProperty>(IRuleBuilderOptions<T, TProperty> builder)
        {
            if (!(builder is RuleBuilder<T, TProperty>))
                throw new ArgumentException("Builder is of wrong type - expecting RuleBuilder<>");
        }
    }

}
