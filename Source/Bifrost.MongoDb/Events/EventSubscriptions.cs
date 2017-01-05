/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Events;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Bifrost.MongoDB.Events
{
    public class EventSubscriptions : IEventSubscriptions
    {
        const string CollectionName = "EventSubscriptions";

        EventStorageConfiguration _configuration;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<EventSubscription> _collection;

        static EventSubscriptions()
        {
            BsonSerializer.RegisterSerializer(typeof(MethodInfo), new MethodInfoSerializer());
            BsonSerializer.RegisterSerializer(typeof(Type), new TypeSerializer());
            BsonSerializer.RegisterSerializationProvider(new BsonSerializationProvider());
        }

        public EventSubscriptions(EventStorageConfiguration configuration)
        {
            _configuration = configuration;
            Initialize();
        }

        void Initialize()
        {
            _server = MongoServer.Create(_configuration.Url);
            _database = _server.GetDatabase(_configuration.DefaultDatabase);
            if (!_database.CollectionExists(CollectionName))
                _database.CreateCollection(CollectionName);

            _collection = _database.GetCollection<EventSubscription>(CollectionName);
        }


        public IEnumerable<EventSubscription> GetAll()
        {
            return _collection.FindAll().ToArray();
        }

        public void Save(EventSubscription subscription)
        {
            _collection.Save(subscription);
        }

        public void ResetLastEventForAllSubscriptions()
        {
            var update = Update.Set("LastEventId",0);
            _collection.Update(Query.Null, update, UpdateFlags.Multi);
        }
    }
}
