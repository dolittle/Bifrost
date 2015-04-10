#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
