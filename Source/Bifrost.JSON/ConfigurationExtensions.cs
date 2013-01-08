using Bifrost.JSON;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.JSON.Serialization;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
		public static IConfigure UsingJson(this ISerializationConfiguration serializationConfiguration) 
		{
			serializationConfiguration.SerializerType = typeof(Serializer);
			return Configure.Instance;
		}
		
        public static IConfigure UsingJson(this ISagasConfiguration sagasConfiguration, string path)
        {
            return Configure.Instance;
        }

        public static IConfigure UsingJson(this IEventsConfiguration eventsConfiguration, string path)
        {
            return Configure.Instance;
        }
            
        public static IConfigure UsingJsonStorage(this IHaveStorage storage, string path)
        {
            var entityContextConfiguration = new EntityContextConfiguration
            {
                Connection = new EntityContextConnection { Directory = path }
            };

            storage.EntityContextConfiguration = entityContextConfiguration;

            return Configure.Instance;
        }
    }
}
