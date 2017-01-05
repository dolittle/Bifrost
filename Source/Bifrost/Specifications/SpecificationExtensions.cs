/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Extensions to help chain simple rules into complex rules
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Combines two atomic rules into a single rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <param name="merge">Expression for merging the two rules</param>
        /// <returns></returns>
        public static Specification<T> Compose<T>(this Specification<T> lhs, Specification<T> rhs, Func<Expression, Expression, Expression> merge)
        {
            return new CompositeRule<T>(lhs, rhs, merge);
        }

        /// <summary>
        /// Combines two rules in to an "And" rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
		public static Specification<T> And<T>(this Specification<T> rhs, Specification<T> lhs)
		{
			return new And<T>(rhs, lhs);
		}


        /// <summary>
        /// Combines two rules in to an "And" rul, where the second rule is negated.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
        public static Specification<T> AndNot<T>(this Specification<T> rhs, Specification<T> lhs)
        {
            return new And<T>(rhs, Is.Not(lhs));
        }

        /// <summary>
        /// Combines two rules in to an "Or" rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <returns>An Or{T} rule"></returns>
		public static Specification<T> Or<T>(this Specification<T> rhs, Specification<T> lhs)
		{
			return new Or<T>(rhs, lhs);
		}

        /// <summary>
        /// Combines two rules into an Or, where the second rule is negated.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rhs">The rule being extended</param>
        /// <param name="lhs">The second rule to be merged into the first</param>
        /// <returns>An And{T} rule"></returns>
        public static Specification<T> OrNot<T>(this Specification<T> rhs, Specification<T> lhs)
        {
            return new Or<T>(rhs, Is.Not(lhs));
        }
    }

}