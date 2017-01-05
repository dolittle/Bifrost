/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Net;
using Raven.Client.Document;

namespace Bifrost.RavenDB
{
    public interface IDocumentStoreConfiguration
    {
        string Url { get; set; }
        string DefaultDatabase { get; set; }
        ICredentials Credentials { get; set; }
        DocumentStore CreateDocumentStore();
        void CopyTo(DocumentStoreConfiguration target);
    }
}
