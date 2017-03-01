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
using MongoDB.Bson;

namespace Bifrost.MongoDb.Events
{
    public class EventSubscriptions : IEventSubscriptions
    {
        const string CollectionName = "EventSubscriptions";

        EventStorageConfiguration _configuration;
        MongoClient _server;
        IMongoDatabase _database;
        IMongoCollection<EventSubscription> _collection;

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
            var s = MongoClientSettings.FromUrl(new MongoUrl(_configuration.Url));
            if (_configuration.UseSSL)
            {
                s.UseSsl = true;
                s.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    CheckCertificateRevocation = false
                };
            }

            _server = new MongoClient(s);

			_database = _server.GetDatabase(_configuration.DefaultDatabase);

            _collection = _database.GetCollection<EventSubscription>(CollectionName);
        }


        public IEnumerable<EventSubscription> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public void Save(EventSubscription subscription)
        {
			var filter = Builders<EventSubscription>.Filter.Eq(s => s.Id, subscription.Id);
			//var update = Builders<EventSubscription>.Update.
            _collection.ReplaceOne(filter, subscription, new UpdateOptions() { IsUpsert  = true });
        }

        public void ResetLastEventForAllSubscriptions()
        {
            var update = Builders<EventSubscription>.Update.Set(s => s.LastEventId, 0);
			
			_collection.UpdateMany(new BsonDocument(), update);
        }
    }
}
