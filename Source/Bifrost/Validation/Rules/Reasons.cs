/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Rules;

namespace Bifrost.Validation.Rules
{
    /// <summary>
    /// Contains common <see cref="BrokenRuleReason">reasons</see> for broken validation rules
    /// </summary>
    public static class Reasons
    {
        /// <summary>
        /// WHen a value is equal and it is not not allowed to be equal, this is the reason given
        /// </summary>
        public static BrokenRuleReason ValueIsEqual = BrokenRuleReason.Create("CEFA9147-5F13-4C82-B609-C64582EC33AB");

        /// <summary>
        /// When a value is less than the specified greater than value, this is the reason given
        /// </summary>
        public static BrokenRuleReason ValueIsLessThan = BrokenRuleReason.Create("8CFB5B51-55E6-41A6-A01A-33F83E141CF2");

        /// <summary>
        /// When a value was greater than the specified less than value, this is the reason given
        /// </summary>
        public static BrokenRuleReason ValueIsGreaterThan = BrokenRuleReason.Create("6C489DB3-DE0A-45BA-A547-5A6E3AD3F303");

        /// <summary>
        /// When something is longer than it should, this is the reason given
        /// </summary>
        public static BrokenRuleReason LengthIsTooLong = BrokenRuleReason.Create("D9675214-A6A4-439F-8D8E-AF0A48BD1BF0");

        /// <summary>
        /// When something is longer than it should, this is the reason given
        /// </summary>
        public static BrokenRuleReason LengthIsTooShort = BrokenRuleReason.Create("E0F8D478-A353-4926-893E-DD367E2F2ACF");
    }
}
