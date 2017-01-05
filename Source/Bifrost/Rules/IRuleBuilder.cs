/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Rules
{
    /// <summary>
    /// Defines the basis for a rule builder
    /// </summary>
    public interface IRuleBuilder<T>
        where T:IRule
    {
        /// <summary>
        /// Gets the rules from the builder
        /// </summary>
        IEnumerable<T> Rules { get; }

        /// <summary>
        /// Add a rule to the builder
        /// </summary>
        /// <param name="rule">A rule that implements <see cref="IRule"/> to add</param>
        void AddRule(T rule);
    }
}
