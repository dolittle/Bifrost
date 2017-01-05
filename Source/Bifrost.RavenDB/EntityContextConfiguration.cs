/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Net;
using Bifrost.Configuration;
using Bifrost.Entities;
using Raven.Client.Document;

namespace Bifrost.RavenDB
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
        public ICredentials Credentials { get; set; }
        public Type EntityContextType { get { return typeof(EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
        public IEntityIdPropertyRegister IdPropertyRegister { get; set; }

        public virtual DocumentStore CreateDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                Url = Url
            };

            if (DefaultDatabase != null)
                documentStore.DefaultDatabase = DefaultDatabase;

            if (Credentials != null)
                documentStore.Credentials = Credentials;

            documentStore.Initialize();

            return documentStore;
        }
    }
}
