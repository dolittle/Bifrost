using System;
using System.Data;
using Oracle.DataAccess.Client;

namespace Bifrost.Oracle.Events
{
    public class EventStoreConfiguration
    {
        public EventStoreConfiguration()
        {
            GetConnection = () => Connection;
        }

        public IDbConnection Connection { get; set; }
        public Func<IDbConnection> GetConnection { get; set; }
    }
}