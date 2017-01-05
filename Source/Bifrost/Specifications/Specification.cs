/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bifrost.Specifications
{
    /// <summary>
    /// Base class for expressing a complex rule in code.  Utilising the Specification pattern. 
    /// </summary>
    /// <typeparam name="T">Type that the rule applies to</typeparam>
    /// <remarks>Based on http://bloggingabout.net/blogs/dries/archive/2011/09/29/specification-pattern-continued.aspx 
    /// </remarks>
    public abstract class Specification<T>
    {
        /// <summary>
        /// Compiled predicate for use against an instance
        /// </summary>
        protected Func<T, bool> evalCompiled;
        /// <summary>
        /// Predicate as an expression for use against IQueryable collection
        /// </summary>
        protected Expression<Func<T, bool>> evalExpression;

        /// <summary>
        /// Predicate rule to be evaluated
        /// </summary>
        protected internal Expression<Func<T, bool>> Predicate
        {
            get
            {
                return evalExpression;
            }
            set
            {
                evalExpression = value;
                evalCompiled = evalExpression.Compile();
            }
        }

        /// <summary>
        /// Evalutes the rule against a single instance of type T.
        /// </summary>
        /// <param name="instance">The instance to evaluation the rule against.</param>
        /// <returns>true if the rule is satisfied, false if the rule is broken</returns>
        public bool IsSatisfiedBy(T instance)
        {
            return evalCompiled(instance);
        }

        /// <summary>
        /// Evaluates the rule against each instance of an IQueryable[T]
        /// </summary>
        /// <param name="candidates">The IQueryable[T] that will have the rule evaluated against each of its instances</param>
        /// <returns>An IQueryable[T] containing only instances that satisfy the rule</returns>
        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return candidates.Where(evalExpression);
        }
    }
}
