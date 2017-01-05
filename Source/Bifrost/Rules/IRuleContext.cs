/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Rules
{
    /// <summary>
    /// Defines the context a rule can evaluate
    /// </summary>
    public interface IRuleContext
    {
        /// <summary>
        /// Register callback that gets called if there is a <see cref="IRule">rule</see> that fails
        /// </summary>
        /// <param name="callback"><see cref="RuleFailed"/> callback</param>
        void OnFailed(RuleFailed callback);

        /// <summary>
        /// Report a rule as failing
        /// </summary>
        /// <param name="rule"><see cref="IRule"/> to report</param>
        /// <param name="instance">The instance that was part of causing the problem</param>
        /// <param name="reason"><see cref="BrokeRuleReason">Reason</see> for it failing</param>
        void Fail(IRule rule, object instance, BrokenRuleReason reason);
    }
}
