/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for specific length - any value must be a specific length
    /// </summary>
    public class MaxLength : ValueRule
    {
        /// <summary>
        /// Initializes an instance of <see cref="Length"/>
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        /// <param name="length">The required length</param>
        public MaxLength(PropertyInfo property, int length) : base(property)
        {
            Length = length;
        }

        /// <summary>
        /// Gets the required length
        /// </summary>
        public int Length { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (FailIfValueTypeMismatch<string>(context, instance))
            {
                var length = ((string)instance).Length;
                if (length > Length) context.Fail(this, instance, Reasons.LengthIsTooLong);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
