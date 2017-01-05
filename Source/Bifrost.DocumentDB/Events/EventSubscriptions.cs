/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Serialization;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptions"/> specific for the Azure DocumentDB
    /// </summary>
    public class EventSubscriptions : IEventSubscriptions
    {
        ISerializer _serializer;

        DocumentClient _client;
        Database _database;
        DocumentCollection _collection;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSubscriptions"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStorageConfiguration">Configuration</see> for event storage</param>
        /// <param name="serializer"><see cref="ISerializer">Serializer</see></param>
        public EventSubscriptions(EventStorageConfiguration configuration, ISerializer serializer)
        {
            _serializer = serializer;

            Initialize(configuration);
            InitializeCollection();
        }

#pragma warning disable 1591
        public IEnumerable<EventSubscription> GetAll()
        {
            var subscriptionsAsDocuments = _client.CreateDocumentQuery(_collection.SelfLink).ToArray();
            var subscriptions = subscriptionsAsDocuments.Select(d => _serializer.FromDocument<EventSubscription>(d));

            return subscriptions;
        }

        public void Save(EventSubscription subscription)
        {
            var document = _client
                .CreateDocumentQuery(_collection.SelfLink).Where(d => d.Id == subscription.Id.ToString()).ToArray()
                .SingleOrDefault(d => d.Id == subscription.Id.ToString());

            using (var stream = _serializer.ToJsonStream(subscription, SerializationExtensions.SerializationOptions))
            {
                if (document != null)
                {
                    _client.ReplaceDocumentAsync(document.SelfLink, Resource.LoadFrom<Document>(stream)).Wait();
                }
                else
                {
                    _client.CreateDocumentAsync(_collection.DocumentsLink, Resource.LoadFrom<Document>(stream)).Wait();
                }
            }
        }

        public void ResetLastEventForAllSubscriptions()
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1591

        void InitializeCollection()
        {
            _collection = null;

            var collectionName = "EventSubscriptions";

            _client.ReadDocumentCollectionFeedAsync(_database.SelfLink)
                .ContinueWith(f => _collection = f.Result.Where(c => c.Id == collectionName).SingleOrDefault())
                .Wait();

            if (_collection == null)
            {
                _collection = new DocumentCollection { Id = collectionName };
                _client
                    .CreateDocumentCollectionAsync(_database.SelfLink, _collection)

                    .ContinueWith(r => _collection = r.Result.Resource)
                    .Wait();
            }
        }


        void Initialize(EventStorageConfiguration configuration)
        {
            _client = new DocumentClient(new Uri(configuration.Url), configuration.AuthorizationKey);
            _client.ReadDatabaseFeedAsync()
                .ContinueWith(a => _database = a.Result.Where(d => d.Id == configuration.DatabaseId).SingleOrDefault())
                .Wait();

            if (_database == null)
            {
                _database = new Database { Id = configuration.DatabaseId };
                _client.CreateDatabaseAsync(_database)
                    .ContinueWith(d => _database = d.Result.Resource)
                    .Wait();
            }
        }

    }
}
