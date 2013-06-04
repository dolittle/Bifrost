using System.Linq.Expressions;

namespace Bifrost.Rules
{
    /// <summary>
    /// Composes a rule that will be satisfied if both the first rule and the second rule are satisfied
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class And<T> : Rule<T>
    {
        internal And(Rule<T> lhs, Rule<T> rhs)
        {
            Predicate = rhs.Compose(lhs, Expression.And).Predicate;
        }
    }
}
