/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation.Internal;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Represents the rule for a model of any type
    /// </summary>
    /// <typeparam name="T">Type the rule represents</typeparam>
    public class ModelRule<T> : PropertyRule
    {
#pragma warning disable 1591 // Xml Comments
        public const string ModelRulePropertyName = "ModelRuleProperty";
        public static string ModelRuleProperty { get; set; }
#pragma warning restore 1591 // Xml Comments
        static PropertyInfo InternalProperty;
        static Func<object, object> InternalFunc = (o) => o; 
        static Expression<Func<T, object>> InternalExpression = (T o) => o; 

        static ModelRule()
        {
            InternalProperty = typeof(ModelRule<T>).GetTypeInfo().GetProperty(ModelRulePropertyName);
        }

        /// <summary>
        /// Initializes an instance of <see cref="ModelRule{T}"/>
        /// </summary>
        public ModelRule()
            : base(InternalProperty, InternalFunc, InternalExpression, () => CascadeMode.StopOnFirstFailure, InternalProperty.PropertyType, typeof(T))
        {
        }
    }
}
