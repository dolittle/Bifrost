/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a way of retrieving single <see cref="IReadModel"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IReadModel"/> it retrieves</typeparam>
    public interface IReadModelOf<T> where T:IReadModel
    {
        /// <summary>
        /// Filter by properties
        /// </summary>
        /// <param name="propertyExpressions">Property filter expressions to use</param>
        /// <returns>An instance or default / null of the <see cref="IReadModel"/>, throws an exception if there is not a unique match</returns>
        T InstanceMatching(params Expression<Func<T, bool>>[] propertyExpressions);
    }
}
