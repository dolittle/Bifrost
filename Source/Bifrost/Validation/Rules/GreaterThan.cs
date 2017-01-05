/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for greater than - any value must be greater than a given value
    /// </summary>
    public class GreaterThan<T> : ValueRule
        where T:IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GreaterThan"/> 
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        /// <param name="value">Value that the input value must be greater than</param>
        public GreaterThan(PropertyInfo property, T value) : base(property)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value that input value must be greater than
        /// </summary>
        public T Value { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (FailIfValueTypeMismatch<T>(context, instance))
            {
                var comparison = ((IComparable<T>)instance).CompareTo(Value);
                if (comparison == 0) context.Fail(this, instance, Reasons.ValueIsEqual);
                if (comparison < 0) context.Fail(this, instance, Reasons.ValueIsLessThan);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
