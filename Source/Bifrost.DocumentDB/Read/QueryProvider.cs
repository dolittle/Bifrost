/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.DocumentDB.Entities;
using Bifrost.Read;
using Microsoft.Azure.Documents.Linq;

namespace Bifrost.DocumentDB.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IQueryProviderFor{T}"/>
    /// </summary>
    public class QueryProvider : IQueryProviderFor<IDocumentQuery>
    {
        EntityContextConnection _connection;

        /// <summary>
        /// Initializes a new instance of <see cref="QueryProvider"/>
        /// </summary>
        /// <param name="connection"><see cref="EntityContextConnection"/> to use for getting to the server</param>
        public QueryProvider(EntityContextConnection connection)
        {
            _connection = connection;
        }


#pragma warning disable 1591 // Xml Comments
        public QueryProviderResult Execute(IDocumentQuery query, PagingInfo paging)
        {
            var queryable = query as IQueryable;
            var result = new QueryProviderResult();

            var collection = _connection.GetCollectionFor(queryable.ElementType);
            _connection.Client.ReadDocumentFeedAsync(collection.DocumentsLink)
                .ContinueWith(r => result.TotalItems)
                .Wait();

            /*
             * Todo: As of 12th of October - this is not supported by the DocumentDB Linq Provider or DocumentDB itself!
            if (paging.Enabled)
            {
                var start = paging.Size * paging.Number;
                queryable = queryable.Skip(start).Take(paging.Size);
            }*/

            result.Items = queryable;

            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
