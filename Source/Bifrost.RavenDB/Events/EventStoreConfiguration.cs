using System.Net;

namespace Bifrost.RavenDB.Events
{
    public class EventStoreConfiguration
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
        public ICredentials Credentials { get; set; }
    }
}
