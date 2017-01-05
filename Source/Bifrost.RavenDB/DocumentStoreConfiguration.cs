/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Net;
using Raven.Client.Document;

namespace Bifrost.RavenDB
{
    public class DocumentStoreConfiguration : IDocumentStoreConfiguration
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
        public ICredentials Credentials { get; set; }

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

            documentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new MethodInfoConverter());
            };

            documentStore.Initialize();

            return documentStore;
        }

        public virtual void CopyTo(DocumentStoreConfiguration target)
        {
            target.Url = Url;
            target.DefaultDatabase = DefaultDatabase;
            target.Credentials = Credentials;
        }
    }
}
