/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Rules;
using Bifrost.Extensions;
using System.Reflection;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for requiring the value
    /// </summary>
    public class Required : ValueRule
    {
        /// <summary>
        /// When a value is null, this is the reason given 
        /// </summary>
        public static BrokenRuleReason ValueIsNull = BrokenRuleReason.Create("712D26C6-A40F-4A3D-8C69-1475E761A1CF");

        /// <summary>
        /// When a value is not specified, this is the reason given
        /// </summary>
        public static BrokenRuleReason StringIsEmpty = BrokenRuleReason.Create("6DE903D6-014C-4B07-B5D3-C3F28677C1A6");

        /// <summary>
        /// When a value is not specified, this is the reason given
        /// </summary>
        public static BrokenRuleReason ValueNotSpecified = BrokenRuleReason.Create("5F790FC3-5C7D-4F3A-B1E9-8F85FAF7176D");

        /// <summary>
        /// Initializes a new instance of the <see cref="Required"/> rule
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        public Required(PropertyInfo property) : base(property) { }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (instance == null) context.Fail(this, instance, ValueIsNull);
            if (instance is string && ((string)instance) == string.Empty) context.Fail(this, instance, StringIsEmpty);

            if (instance != null)
            {
                var type = instance.GetType();
                if (type.HasDefaultConstructor())
                {
                    if (Activator.CreateInstance(type).Equals(instance)) context.Fail(this, instance, ValueNotSpecified);
                }
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
