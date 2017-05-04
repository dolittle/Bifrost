/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read.MongoDB.Concepts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bifrost.Read.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContextConnection"/> for MongoDB
    /// </summary>
    public class EntityContextConnection : IEntityContextConnection
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EntityContextConnection"/>
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration">Configuration</see></param>
        /// <param name="classMaps">Instances of <see cref="BsonClassMap"/></param>
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

        /// <summary>
        /// Gets the <see cref="MongoClient"/> representing the connection to the server
        /// </summary>
        public MongoClient Server { get; }

        /// <summary>
        /// Gets the <see cref="IMongoDatabase"/> - a reference to the actual database
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Initialize the connection
        /// </summary>
        /// <param name="container"></param>
        public void Initialize(IContainer container)
        {
            var classMaps = container.Get<IInstancesOf<BsonClassMap>>();

            classMaps.ForEach(BsonClassMap.RegisterClassMap);
        }
    }
}