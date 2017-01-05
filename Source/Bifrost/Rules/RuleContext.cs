/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Rules
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuleContext"/>
    /// </summary>
    public class RuleContext : IRuleContext
    {
        List<RuleFailed> _callbacks = new List<RuleFailed>();

#pragma warning disable 1591 // Xml Comments
        public void OnFailed(RuleFailed callback)
        {
            _callbacks.Add(callback);
        }

        public void Fail(IRule rule, object instance, BrokenRuleReason reason)
        {
            _callbacks.ForEach(c => c(rule, instance, reason));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
