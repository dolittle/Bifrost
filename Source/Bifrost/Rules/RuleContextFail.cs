/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Rules
{
    /// <summary>
    /// Delegate that gets called when a <see cref="IRule"/> fails
    /// </summary>
    /// <param name="rule"><see cref="IRule"/> that is failing</param>
    /// <param name="instance">Instance it was evaluating</param>
    /// <param name="reason"><see cref="BrokenRuleReason">Reason</see> for failing</param>
    public delegate void RuleFailed(IRule rule, object instance, BrokenRuleReason reason);
}
