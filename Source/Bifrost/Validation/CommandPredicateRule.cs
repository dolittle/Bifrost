#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Reflection;
using Bifrost.Commands;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
#if(NETFX_CORE)
using System.Linq;
#endif

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents the rule for a predicate in the form of a Func that will be called for validation
    /// </summary>
    /// <typeparam name="T">Type of command the validation applies to</typeparam>
    public class CommandPredicateRule<T> : PropertyRule
        where T:ICommand
    {
#if(NETFX_CORE)
        static MemberInfo IdProperty = typeof(ICommand).GetTypeInfo().DeclaredProperties.Where(p=>p.Name == "Id").Single();
#else
        static MemberInfo IdProperty = typeof(ICommand).GetProperty("Id");
#endif
        static Func<object, object> IdFunc;
        static Expression<Func<ICommand, Guid>> IdFuncExpression;

        static CommandPredicateRule()
        {
            Func<ICommand, Guid> idFunc = cmd => cmd.Id;
            IdFuncExpression = (ICommand cmd) => cmd.Id;

            IdFunc = idFunc.CoerceToNonGeneric();
        }


        Func<T, bool> _validateFor;


        /// <summary>
        /// Creates an instance of the <see cref="CommandPredicateRule(T)"/>
        /// </summary>
        /// <param name="validateFor"><see cref="Func(T,bool)"/> that will be called for validation</param>
        public CommandPredicateRule(Func<T, bool> validateFor)
            : base(IdProperty, IdFunc, IdFuncExpression, () => CascadeMode.StopOnFirstFailure, typeof(T), typeof(T))
        {
            _validateFor = validateFor;
            AddValidator(new PredicateValidator((o, p, c) => true));
        }


#pragma warning disable 1591 // Xml Comments
        public override IEnumerable<ValidationFailure> Validate(ValidationContext context)
        {
            if (!_validateFor((T)context.InstanceToValidate))
                return new[] {
                    new ValidationFailure(string.Empty,CurrentValidator.ErrorMessageSource.GetString())
                };

            return new ValidationFailure[0];
        }
#pragma warning restore 1591 // Xml Comments


        /// <summary>
        /// Create a <see cref="CommandPredicateRule(T)"/> from a <see cref="Func(T, bool)"/> to use for validation
        /// </summary>
        /// <param name="validateFor"><see cref="Func(T, bool)"/> to use for validation</param>
        /// <returns>A <see cref="CommandPredicateRule(T)"/></returns>
        public static CommandPredicateRule<T> Create(Func<T, bool> validateFor)
        {
            var rule = new CommandPredicateRule<T>(validateFor);
            return rule;
        }
    }
}
