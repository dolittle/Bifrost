using Bifrost.Configuration;
using Bifrost.Configuration.Xml;

namespace Bifrost.JSON.Configuration
{
    [ElementName("Json")]
    public class JsonStorageElement : StorageElement
    {
        public string Directory { get; set; }

        public JsonStorageElement()
        {
            EntityContextType = typeof(EntityContext<>);
        }

        public override IEntityContextConfiguration GetConfiguration()
        {
            var configuration = new EntityContextConfiguration();
            var connection = new EntityContextConnection {Directory = Directory};
            configuration.Connection = connection;
            return configuration;
        }
    }
}
