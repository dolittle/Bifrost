/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Composes a rule that will be satisfied if either the first rule or second rule is satisfied
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class Or<T> : Specification<T>
	{
		internal Or(Specification<T> lhs, Specification<T> rhs)
		{
			Predicate = lhs.Compose(rhs, Expression.Or).Predicate;
		}
	}
}