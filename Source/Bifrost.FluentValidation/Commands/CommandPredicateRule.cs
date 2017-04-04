/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Commands;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation.Commands
{
    /// <summary>
    /// Represents the rule for a predicate in the form of a Func that will be called for validation
    /// </summary>
    /// <typeparam name="T">Type of command the validation applies to</typeparam>
    public class CommandPredicateRule<T> : PropertyRule
        where T:ICommand
    {
        static MemberInfo IdProperty = typeof(ICommand).GetTypeInfo().GetProperty("Id");
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
        /// Creates an instance of the <see cref="CommandPredicateRule{T}"/>
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T,TR}"/> that will be called for validation</param>
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
                    new ValidationFailure(string.Empty,CurrentValidator.ErrorMessageSource.GetString(null))
                };

            return new ValidationFailure[0];
        }
#pragma warning restore 1591 // Xml Comments


        /// <summary>
        /// Create a <see cref="CommandPredicateRule{T}"/> from a <see cref="Func{T, TR}"/> to use for validation
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T, TR}"/> to use for validation</param>
        /// <returns>A <see cref="CommandPredicateRule{T}"/></returns>
        public static CommandPredicateRule<T> Create(Func<T, bool> validateFor)
        {
            var rule = new CommandPredicateRule<T>(validateFor);
            return rule;
        }
    }
}
