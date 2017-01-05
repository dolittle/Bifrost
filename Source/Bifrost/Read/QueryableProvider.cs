/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of a <see cref="IQueryProviderFor{T}"/> for <see cref="IQueryable"/>
    /// </summary>
    public class QueryableProvider : IQueryProviderFor<IQueryable>
    {
#pragma warning disable 1591 // Xml Comments
        public QueryProviderResult Execute(IQueryable query, PagingInfo paging)
        {
            var result = new QueryProviderResult();

            result.TotalItems = query.Count();

            if (paging.Enabled)
            {
                var start = paging.Size * paging.Number;
                var end = paging.Size;
                if (query.IsTakeEndIndex()) end += start;
                query = query.Skip(start).Take(end);
            }

            result.Items = query;

            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
