/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Read
{
    /// <summary>
    /// Defines a provider that can deal with a query for
    /// </summary>
    public interface IQueryProviderFor<T>
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
