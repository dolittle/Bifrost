/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bifrost.Validation;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Extensions to the <see cref="ICanValidate"/> interface to support the FluentValidation concept of RuleSets.
    /// </summary>
    public static class ICanValidateExtensions
    {
        /// <summary>
        /// Allows a ruleset to be specified for the validation
        /// </summary>
        /// <param name="validator">Instance of the validator being extended</param>
        /// <param name="target">Instance of the object to be validated</param>
        /// <param name="ruleSet"></param>
        /// <param name="includeDefaultRuleset">Optional parameter indicating whether the default ruleset should be used.  True by default.</param>
        /// <returns>An enumeration of ValidationResults.  An entry for each validation error or an empty enumeration for Valid.</returns>
        public static IEnumerable<ValidationResult> ValidateFor(this ICanValidate validator, object target, string ruleSet, bool includeDefaultRuleset = true)
        {
            var rulesets = includeDefaultRuleset ? "default," + ruleSet : ruleSet;
            var method = validator.GetType().GetTypeInfo().GetMethods().First(m => m.Name == "ValidateFor" && m.GetParameters().Count() == 2);
            //var genericMethod = method.MakeGenericMethod(target.GetType());
            return (IEnumerable<ValidationResult>)method.Invoke(validator, new[] { target, rulesets });
        }
    }
}
