using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class EventStoreConfiguration
    {
        public OracleConnection Connection { get; set; }
    }
}