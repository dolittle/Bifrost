/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for requiring the value to not be null
    /// </summary>
    public class NotNull : ValueRule
    {
        /// <summary>
        /// When a value is null, this is the reason given 
        /// </summary>
        public static BrokenRuleReason ValueIsNull = BrokenRuleReason.Create("712D26C6-A40F-4A3D-8C69-1475E761A1CF");

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNull"/> rule
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        public NotNull(PropertyInfo property) : base(property) { }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (instance == null) context.Fail(this, instance, ValueIsNull);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
