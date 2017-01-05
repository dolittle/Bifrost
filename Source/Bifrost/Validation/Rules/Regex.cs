/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for specific regular expression - any value must conform with a regular expression
    /// </summary>
    public class Regex : ValueRule
    {
        /// <summary>
        /// When a string does not conform to the specified expression, this is the reason given
        /// </summary>
        public static BrokenRuleReason NotConformingToExpression = BrokenRuleReason.Create("BE58A125-40DB-47EA-B260-37F7AF4455C5");

        System.Text.RegularExpressions.Regex _actualRegex;

        /// <summary>
        /// Initializes an instance of <see cref="Regex"/>
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        /// <param name="expression"></param>
        public Regex(PropertyInfo property, string expression) : base(property)
        {
            Expression = expression;
            _actualRegex = new System.Text.RegularExpressions.Regex(expression);
        }

        /// <summary>
        /// Get the expression that values must conform to
        /// </summary>
        public string Expression { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (FailIfValueTypeMismatch<string>(context, instance))
            {
                if (!_actualRegex.IsMatch((string)instance)) context.Fail(this, instance, NotConformingToExpression);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
