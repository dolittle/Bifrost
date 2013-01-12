using System.Net;

namespace Bifrost.MongoDB.Events
{
    public class EventStoreConfiguration
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
    }
}
