/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Specifications
{
    /// <summary>
    /// Helps chain simple <see cref="Specification{T}"/> together
    /// </summary>
    public static class Is
    {
        /// <summary>
        /// Creates a Not rule based on the rule passed in.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rule">The rule being extended</param>
        /// <returns>A Not{T} rule"></returns>
        public static Specification<T> Not<T>(Specification<T> rule)
        {
            return new Negative<T>(rule);
        }
    }
}
