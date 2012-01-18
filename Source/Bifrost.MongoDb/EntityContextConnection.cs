using Bifrost.Entities;
using MongoDB.Driver;

namespace Bifrost.MongoDB
{
    public class EntityContextConnection : IEntityContextConnection
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public MongoServer Server { get; private set; }
        public MongoDatabase Database { get; private set; }

        public EntityContextConnection(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;

            Server = MongoServer.Create(connectionString);
            Database = Server.GetDatabase(databaseName);
        }
    }
}
