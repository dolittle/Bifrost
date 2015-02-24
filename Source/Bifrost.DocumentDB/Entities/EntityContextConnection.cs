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
using Bifrost.Entities;
using Bifrost.Execution;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Bifrost.DocumentDB.Entities
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContextConnection"/>
    /// </summary>
    public class EntityContextConnection : IEntityContextConnection
    {
        EntityContextConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="EntityContextConnection"/>
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration">Configuration</see> to use</param>
        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the <see cref="DocumentClient"/> for the connection
        /// </summary>
        public DocumentClient Client { get; private set; }

        /// <summary>
        /// Gets the <see cref="Database"/> for the connection
        /// </summary>
        public Database Database { get; private set; }


        /// <summary>
        /// Get a <see cref="DocumentCollection"/> for a specific type
        /// </summary>
        /// <typeparam name="T">Type to get collection for</typeparam>
        /// <returns>The <see cref="DocumentCollection"/> for the type</returns>
        public DocumentCollection GetCollectionFor<T>()
        {
            return GetCollectionFor(typeof(T));
        }


        /// <summary>
        /// Get a <see cref="DocumentCollection"/> for a specific type
        /// </summary>
        /// <param name="type">Type to get collection for</param>
        /// <returns>The <see cref="DocumentCollection"/> for the type</returns>
        public DocumentCollection GetCollectionFor(Type type)
        {
            return GetCollectionFor(type.Name);
        }

        /// <summary>
        /// Get a <see cref="DocumentCollection"/> for a specific name
        /// </summary>
        /// <param name="name">Name of collection</param>
        /// <returns>The <see cref="DocumentCollection"/></returns>
        public DocumentCollection GetCollectionFor(string name)
        {
            DocumentCollection collection = null;

            

            Client.ReadDocumentCollectionFeedAsync(Database.SelfLink)
                .ContinueWith(f => collection = f.Result.Where(c => c.Id == name).SingleOrDefault())
                .Wait();

            if (collection == null)
            {
                collection = new DocumentCollection { Id = name };
                Client
                    .CreateDocumentCollectionAsync(Database.SelfLink, collection)
                    .ContinueWith(r => collection = r.Result.Resource)
                    .Wait();
            }

            return collection;

        }

#pragma warning disable 1591 // Xml Comments
        public void Initialize(IContainer container)
        {
            Client = new DocumentClient(new Uri(_configuration.Url), _configuration.AuthorizationKey);

            Client.ReadDatabaseFeedAsync()
                .ContinueWith(a => Database = a.Result.Where(d => d.Id == _configuration.DatabaseId).SingleOrDefault())
                .Wait();

            if (Database == null)
            {
                this.Database = new Database { Id = _configuration.DatabaseId };
                Client.CreateDatabaseAsync(Database)
                    .ContinueWith(d=>Database = d.Result.Resource)
                    .Wait();
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
