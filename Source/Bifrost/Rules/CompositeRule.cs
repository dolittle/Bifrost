using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bifrost.Rules
{
    /// <summary>
    /// Composes two rules into a single rule that can be evaluated atomically.
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class CompositeRule<T> : Rule<T>
	{
		internal CompositeRule(Rule<T> lhs, Rule<T> rhs, Func<Expression, Expression, Expression> merge)
		{
            var map = lhs.Predicate.Parameters.Select((f, i) => new { f, s = rhs.Predicate.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
			var secondBody = ParameterRebinder.ReplaceParameters(map, rhs.Predicate.Body);
			Predicate = Expression.Lambda<Func<T, bool>>(merge(lhs.Predicate.Body, secondBody), lhs.Predicate.Parameters);
		}
	}
}