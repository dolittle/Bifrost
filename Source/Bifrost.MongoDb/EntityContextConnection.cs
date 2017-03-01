/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.MongoDb.Concepts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bifrost.MongoDb
{
    public class EntityContextConnection : IEntityContextConnection
    {
        public MongoClient Server { get; private set; }
        public IMongoDatabase Database { get; private set; }

        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            var s = MongoClientSettings.FromUrl(new MongoUrl(configuration.Url));
            if (configuration.UseSSL)
            {
                s.UseSsl = true;
                s.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    CheckCertificateRevocation = false
                };
            }

            Server = new MongoClient(s);
            Database = Server.GetDatabase(configuration.DefaultDatabase);

            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        public void Initialize(IContainer container)
        {
            
        }
    }
}
