/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a provider that can deal with a query for <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered and called whenever a <see cref="IQuery"/>
    /// with a Query property of type <typeparamref name="T"/> is encountered.
    /// </remarks>
    public interface IQueryProviderFor<T> : IConvention
    {
        /// <summary>
        /// Execute a query 
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="paging"><see cref="PagingInfo"/> to apply</param>
        /// <returns><see cref="QueryResult">Result</see> from the query</returns>
        QueryProviderResult Execute(T query, PagingInfo paging);
    }
}
