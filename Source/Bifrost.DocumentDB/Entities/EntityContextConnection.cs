/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
        /// <param name="collectionStrategy"><see cref="ICollectionStrategy"/> to use for types</param>
        public EntityContextConnection(EntityContextConfiguration configuration, ICollectionStrategy collectionStrategy)
        {
            _configuration = configuration;
            CollectionStrategy = collectionStrategy;
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
        /// Gets the <see cref="ICollectionStrategy"/> used
        /// </summary>
        public ICollectionStrategy CollectionStrategy { get; private set; }


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
            var name = CollectionStrategy.CollectionNameFor(type);
            return GetCollectionFor(name);
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
                .ContinueWith(f => {
                    var collections = f.Result.ToArray();
                    var cc = collections.Where(c => c.Id == name).SingleOrDefault();
                    collection = cc;
                        
                })
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
