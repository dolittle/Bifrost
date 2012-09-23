#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
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
    /// Base class to inherit from for basic validation rules
    /// </summary>
    /// <typeparam name="T">Type to add validation rules for</typeparam>
    public class Validator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Start building rules for the model
        /// </summary>
        /// <returns><see cref="IRuleBuilderInitial(T, T)"/> that can be used to fluently set up rules</returns>
        public IRuleBuilderInitial<T, T> ModelRule()
        {
            var rule = new ModelRule<T>();
            AddRule(rule);
            var builder = new RuleBuilder<T, T>(rule);
            return builder;
        }


        /// <summary>
        /// Defines a validation rule for a specific property and adding the posibility for associating dynamic data with it
        /// </summary>
        /// <example>
        /// RuleWithDynamicDataFor(x => x.Surname)...
        /// </example>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="expression">The expression representing the property to validate</param>
        /// <returns>an <see cref="DynamicStateRuleBuilder{T, TProperty}"/> instance on which validators can be defined</returns>
        public DynamicStateRuleBuilder<T, TProperty> RuleWithDynamicDataFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var rule = PropertyRule.Create(expression, () => CascadeMode);
            AddRule(rule);
            var ruleBuilder = new DynamicStateRuleBuilder<T, TProperty>(rule);
            return ruleBuilder;
        }
    }
}
