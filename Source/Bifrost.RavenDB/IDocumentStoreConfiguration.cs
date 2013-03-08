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
