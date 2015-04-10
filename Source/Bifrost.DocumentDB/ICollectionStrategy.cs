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
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB
{
    /// <summary>
    /// Defines a strategy to be used for dealing with entities and their relation to collections
    /// </summary>
    public interface ICollectionStrategy
    {
        /// <summary>
        /// Gets the name of the collection based on the type
        /// </summary>
        /// <typeparam name="T">Type to get collection name for</typeparam>
        /// <returns>Name of the collection</returns>
        string CollectionNameFor<T>();

        /// <summary>
        /// Gets the name of the collection based on the type
        /// </summary>
        /// <param name="type">Type to get collection name for</param>
        /// <returns>Name of the collection</returns>
        string CollectionNameFor(Type type);

        /// <summary>
        /// Handle queryable for a specific type
        /// </summary>
        /// <typeparam name="T">Type to handle queryable for</typeparam>
        /// <param name="queryable">Queryable to handle</param>
        /// <returns>Handled queryable</returns>
        IQueryable<T> HandleQueryableFor<T>(IQueryable<T> queryable);

        /// <summary>
        /// Handle document for a specific type
        /// </summary>
        /// <typeparam name="T">Type to handle for</typeparam>
        /// <param name="document">Document to handle</param>
        void HandleDocumentFor<T>(Document document);
    }
}
