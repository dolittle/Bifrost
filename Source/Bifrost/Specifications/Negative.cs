/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Negates a rule.  Rule is satisfied if the provided rule is not satisfied.
    /// </summary>
    /// <typeparam name="T">Type that the rule is to be evalued for.</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx </remarks>
    internal class Negative<T> : Specification<T>
    {
        internal Negative(Specification<T> rule)
        {
            Predicate = Expression.Lambda<Func<T, bool>>(Expression.Not(rule.Predicate.Body), rule.Predicate.Parameters);
        }
    }
}