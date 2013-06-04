using System.Linq.Expressions;
namespace Bifrost.Rules
{
    /// <summary>
    /// Composes a rule that will be satisfied if either the first rule or second rule is satisfied
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class Or<T> : Rule<T>
	{
		internal Or(Rule<T> lhs, Rule<T> rhs)
		{
			Predicate = lhs.Compose(rhs, Expression.Or).Predicate;
		}
	}
}