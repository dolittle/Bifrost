/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.MongoDB.Concepts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bifrost.MongoDB
{
    public class EntityContextConnection : IEntityContextConnection
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public MongoServer Server { get; private set; }
        public MongoDatabase Database { get; private set; }

        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            ConnectionString = configuration.Url;
            DatabaseName = configuration.DefaultDatabase;

            Server = MongoServer.Create(ConnectionString);
            Database = Server.GetDatabase(DatabaseName);
            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        public void Initialize(IContainer container)
        {
            
        }
    }
}
