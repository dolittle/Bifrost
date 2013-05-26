using System;
using System.Linq.Expressions;

namespace Bifrost.Rules
{
    /// <summary>
    /// Extensions to help chain simple rules into complex rules
    /// </summary>
    public static class RuleExtensions
    {
        /// <summary>
        /// Combines two atomic rules into a single rule
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="lhs">The rule being extended</param>
        /// <param name="rhs">The second rule to be merged into the first</param>
        /// <param name="merge">Expression for merging the two rules</param>
        /// <returns></returns>
        public static Rule<T> Compose<T>(this Rule<T> lhs, Rule<T> rhs, Func<Expression, Expression, Expression> merge)
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
		public static Rule<T> And<T>(this Rule<T> rhs, Rule<T> lhs)
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
        public static Rule<T> AndNot<T>(this Rule<T> rhs, Rule<T> lhs)
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
		public static Rule<T> Or<T>(this Rule<T> rhs, Rule<T> lhs)
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
        public static Rule<T> OrNot<T>(this Rule<T> rhs, Rule<T> lhs)
        {
            return new Or<T>(rhs, Is.Not(lhs));
        }
    }

    /// <summary>
    /// Helps chain simple <see cref="Rule{T}"/> together
    /// </summary>
    public static class Is
    {
        /// <summary>
        /// Creates a Not rule based on the rule passed in.
        /// </summary>
        /// <typeparam name="T">Type of the instance that the rule is to be evaluated against</typeparam>
        /// <param name="rule">The rule being extended</param>
        /// <returns>A Not{T} rule"></returns>
        public static Rule<T> Not<T>(Rule<T> rule)
        {
            return new Negative<T>(rule);
        }
    }
}