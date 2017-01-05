/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Composes a rule that will be satisfied if both the first rule and the second rule are satisfied
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evaluated for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class And<T> : Specification<T>
    {
        internal And(Specification<T> lhs, Specification<T> rhs)
        {
            Predicate = rhs.Compose(lhs, Expression.And).Predicate;
        }
    }
}
