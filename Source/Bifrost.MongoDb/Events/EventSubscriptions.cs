#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
            subscription.Id = subscription.GetHashCode();
            _collection.Save(subscription);
        }

        public void ResetLastEventForAllSubscriptions()
        {
            var update = Update.Set("LastEventId",0);
            _collection.Update(Query.Null, update, UpdateFlags.Multi);
        }
    }
}
