/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Extensions;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Represents a <see cref="PropertyValidator"/> that can hold dynamic state
    /// </summary>
    public abstract class PropertyValidatorWithDynamicState : PropertyValidator
    {
        List<PropertyInfo> _properties = new List<PropertyInfo>();

#pragma warning disable 1591 // Xml Comments
        protected PropertyValidatorWithDynamicState(Expression<Func<string>> errorMessageResourceSelector) : base(errorMessageResourceSelector) { }
        protected PropertyValidatorWithDynamicState(string errorMessage) : base(errorMessage) { }
        protected PropertyValidatorWithDynamicState(string errorMessageResourceName, Type errorMessageResourceType) : base(errorMessageResourceName, errorMessageResourceType) { }


        protected dynamic DynamicState { get; private set; }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            DynamicState = new DynamicState(context.Instance, Properties);
            return base.Validate(context);
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Properties representing the dynamic state
        /// </summary>
        public IEnumerable<PropertyInfo> Properties { get { return _properties; } }

        /// <summary>
        /// Add an expression that resolve to a property
        /// </summary>
        /// <param name="expression">Expression to add</param>
        public virtual void AddExpression<T>(Expression<Func<T,object>> expression)
        {
            var property = expression.GetPropertyInfo();
            _properties.Add(property);
        }
    }
}
