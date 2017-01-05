/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Rules
{
    /// <summary>
    /// Defines the basis for a rule
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Evaluates the given <see cref="IRuleContext"/> to see if the rule is satisfied
        /// </summary>
        /// <param name="context">The <see cref="IRuleContext"/> to evaluate for</param>
        /// <param name="instance">The instance to check if satisfies the rule</param>
        void Evaluate(IRuleContext context, object instance);
    }
}
