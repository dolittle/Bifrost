/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Represents the <see cref="ValueRule"/> for email - any value must be a valid email
    /// </summary>
    public class Email : ValueRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> rule
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo">Property</see> the rule is for</param>
        public Email(PropertyInfo property) : base(property) { }

        /// <summary>
        /// When an email is invalid, this is the reason given
        /// </summary>
        public static BrokenRuleReason InvalidEMailReason = BrokenRuleReason.Create("A62F369F-9C92-4A06-96C3-654AB0E15119");

        const string _expression = @"((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        static System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex(_expression);


#pragma warning disable 1591 // Xml Comments
        public override void Evaluate(IRuleContext context, object instance)
        {
            if (FailIfValueTypeMismatch<string>(context, instance))
            {
                if (!_regex.IsMatch((string)instance)) context.Fail(this, instance, InvalidEMailReason);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
